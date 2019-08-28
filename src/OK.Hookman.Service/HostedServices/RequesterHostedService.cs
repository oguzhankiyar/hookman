using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OK.Hookman.Core.Enums;
using OK.Hookman.Persistence.Core.Repositories;

namespace OK.Hookman.Service.HostedServices
{
    public interface IRequesterQueue
    {
        void Add(int hookId);
        int Take();
    }

    public class RequesterQueue : IRequesterQueue
    {
        private ConcurrentQueue<int> _hookIds = new ConcurrentQueue<int>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void Add(int hookId)
        {
            _hookIds.Enqueue(hookId);
            _signal.Release();
        }

        public int Take()
        {
            _signal.Wait();
            _hookIds.TryDequeue(out var hookId);
            return hookId;
        }
    }

    public class RequesterHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public IRequesterQueue _requesterQueue { get; }

        public RequesterHostedService(IServiceScopeFactory serviceScopeFactory, IRequesterQueue requesterQueue)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _requesterQueue = requesterQueue ?? throw new ArgumentNullException(nameof(requesterQueue));
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var hookId = _requesterQueue.Take();

                try
                {
                    await SendHookAsync(hookId);
                }
                catch (Exception)
                {
                }
            }
        }

        private async Task SendHookAsync(int hookId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var hookRepository = scope.ServiceProvider.GetRequiredService<IHookRepository>();

                var entity = hookRepository.FindOne(x => x.Id == hookId);

                var data = entity.Data;
                var retryCount = entity.Event.RetryCount;

                var headers = new Dictionary<string, string>();
                var queryStrings = new Dictionary<string, string>();

                if (!string.IsNullOrEmpty(entity.Event.Receiver.Headers))
                {
                    headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(entity.Event.Receiver.Headers);
                    if (headers == null)
                    {
                        headers = new Dictionary<string, string>();
                    }
                }

                if (!string.IsNullOrEmpty(entity.Event.Headers))
                {
                    foreach (var hd in JsonConvert.DeserializeObject<Dictionary<string, string>>(entity.Event.Headers))
                    {
                        if (headers.ContainsKey(hd.Key))
                        {
                            headers[hd.Key] = hd.Value;
                        }
                        else
                        {
                            headers.Add(hd.Key, hd.Value);
                        }
                    }
                }

                headers = headers.ToDictionary(x => x.Key, x => InjectData(x.Value, data));

                if (!string.IsNullOrEmpty(entity.Event.Receiver.QueryStrings))
                {
                    queryStrings = JsonConvert.DeserializeObject<Dictionary<string, string>>(entity.Event.Receiver.QueryStrings);
                    if (queryStrings == null)
                    {
                        queryStrings = new Dictionary<string, string>();
                    }
                }

                if (!string.IsNullOrEmpty(entity.Event.QueryStrings))
                {
                    foreach (var qs in JsonConvert.DeserializeObject<Dictionary<string, string>>(entity.Event.QueryStrings))
                    {
                        if (queryStrings.ContainsKey(qs.Key))
                        {
                            queryStrings[qs.Key] = qs.Value;
                        }
                        else
                        {
                            queryStrings.Add(qs.Key, qs.Value);
                        }
                    }
                }

                var url = entity.Event.Receiver.Url.Trim().TrimEnd('/') + "/";
                if (!string.IsNullOrEmpty(entity.Event.Receiver.Path?.Trim('/')))
                {
                    url += entity.Event.Receiver.Path.Trim().Trim('/') + "/";
                }
                if (!string.IsNullOrEmpty(entity.Event.Path?.TrimStart('/').TrimEnd('?')))
                {
                    url += entity.Event.Path.Trim().TrimStart('/').TrimEnd('?');
                }
                if (queryStrings.Count > 0)
                {
                    url += "?" + string.Join("&", queryStrings.Select(qs => qs.Key + "=" + qs.Value));
                }

                url = InjectData(url, entity.Data);

                var body = string.Empty;
                if (!string.IsNullOrEmpty(entity.Event.Body))
                {
                    body = InjectData(entity.Event.Body, data);
                }

                entity.RequestUrl = url;
                entity.RequestHeaders = JsonConvert.SerializeObject(headers);
                entity.RequestBody = body;

                try
                {
                    var response = await SendRequest(entity.Event.Method, url, headers, body, retryCount);

                    entity.ResponseCode = response.StatusCode;
                    entity.ResponseHeaders = JsonConvert.SerializeObject(response.Headers.ToDictionary(k => k.Key, v => string.Join("|", v.Value)));
                    entity.ResponseBody = response.Content;
                    entity.StatusId = (int)StatusEnum.Sent;
                    entity.Message = StatusEnum.Sent.ToString();
                }
                catch (Exception ex)
                {
                    entity.StatusId = (int)StatusEnum.Failed;
                    entity.Message = ex.ToString();
                }

                hookRepository.Update(entity);
            }
        }

        private async Task<(int StatusCode, string Content, Dictionary<string, string[]> Headers)> SendRequest(string method, string url, IDictionary<string, string> headers, string body, int retryCount)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = method;
            foreach (var hd in headers)
            {
                request.Headers.Add(hd.Key, hd.Value);
            }

            if (!string.IsNullOrEmpty(body))
            {
                using (var stream = request.GetRequestStream())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(body);
                    writer.Flush();
                }
            }

            var response = null as HttpWebResponse;

            try
            {
                response = (await request.GetResponseAsync()) as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;

                if (response == null)
                {
                    throw;
                }

                retryCount--;

                if (retryCount > 0)
                {
                    return await SendRequest(method, url, headers, body, retryCount);
                }
            }

            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var responseStatusCode = (int)response.StatusCode;
                var responseContent = reader.ReadToEnd();
                var responseHeaders = response.Headers.AllKeys.ToDictionary(x => x, x => response.Headers.GetValues(x));

                return (responseStatusCode, responseContent, responseHeaders);
            }
        }

        private string InjectData(string content, string data)
        {
            if (content == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(data))
            {
                return content;
            }

            var injected = content;
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);

            injected = injected.Replace("{{JSON(Model)}}", data);

            foreach (var item in dict)
            {
                injected = injected.Replace("{{Model." + item.Key + "}}", item.Value?.ToString());
            }

            return injected;
        }
    }
}