using System;
using OK.Hookman.Client.Config;
using Refit;

namespace OK.Hookman.Client.Factory
{
    public class HookmanClientFactory : IHookmanClientFactory
    {
        private readonly HookmanClientConfig _config;

        public HookmanClientFactory(HookmanClientConfig config) =>
            _config = config ?? throw new ArgumentNullException(nameof(Config));

        public IHookmanClient CreateClient() =>
            RestService.For<IHookmanClient>(_config.ApiUrl);
    }
}