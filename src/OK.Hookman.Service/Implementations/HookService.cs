using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OK.Hookman.Core.Enums;
using OK.Hookman.Core.Exceptions;
using OK.Hookman.Core.Requests.Hook;
using OK.Hookman.Core.Responses.Hook;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Service.Abstractions;
using OK.Hookman.Service.Base;
using OK.Hookman.Service.HostedServices;

namespace OK.Hookman.Service.Implementations
{
    public class HookService : BaseService, IHookService
    {
        private readonly IHookRepository _hookRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ISenderRepository _senderRepository;
        private readonly IMapper _mapper;

        public IRequesterQueue _requesterQueue { get; }

        public HookService(
            IHookRepository hookRepository,
            IEventRepository eventRepository,
            ISenderRepository senderRepository,
            IRequesterQueue requesterQueue,
            IMapper mapper,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {

            _hookRepository = hookRepository ?? throw new ArgumentNullException(nameof(hookRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _senderRepository = senderRepository ?? throw new ArgumentNullException(nameof(senderRepository));
            _requesterQueue = requesterQueue ?? throw new ArgumentNullException(nameof(requesterQueue));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<HookListResponse> GetHooksAsync(HookListRequest request)
        {
            await ValidateRequestAsync(request);

            var skip = request.PageSize * (request.PageNumber - 1);
            var take = request.PageSize;

            var entities = _hookRepository.FindAll().OrderByDescending(x => x.CreatedDate).Skip(skip).Take(take).ToList();
            var recordCount = _hookRepository.Count();

            var response = _mapper.Map<HookListResponse>(entities);
            response.PageSize = request.PageSize;
            response.PageNumber = request.PageNumber;
            response.RecordCount = recordCount;
            response.PageCount = (int)Math.Ceiling((double)response.RecordCount / response.PageSize);
            return response;
        }

        public async Task<HookDetailResponse> GetHookAsync(HookDetailRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _hookRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<HookDetailResponse>(entity);
        }

        public async Task<HookCreateResponse> CreateHookAsync(HookCreateRequest request)
        {
            await ValidateRequestAsync(request);

            var sender = _senderRepository.FindOne(x => x.Token == request.SenderToken);

            if (request.EventId.HasValue)
            {
                var entity = _hookRepository.Insert(new HookEntity()
                {
                    EventId = request.EventId.Value,
                    SenderId = sender.Id,
                    Data = request.Data,
                    StatusId = (int)StatusEnum.Created,
                    Message = StatusEnum.Created.ToString()
                });

                _requesterQueue.Add(entity.Id);

                return new HookCreateResponse() { Status = true, Message = "Created 1 hook(s)" };
            }
            else if (request.ActionId.HasValue)
            {
                var events = _eventRepository.FindMany(x => x.ActionId == request.ActionId.Value && (x.SenderId == null || x.SenderId == sender.Id));

                foreach (var item in events)
                {
                    var entity = _hookRepository.Insert(new HookEntity()
                    {
                        EventId = item.Id,
                        SenderId = sender.Id,
                        Data = request.Data,
                        StatusId = (int)StatusEnum.Created,
                        Message = StatusEnum.Created.ToString()
                    });

                    _requesterQueue.Add(entity.Id);
                }

                return new HookCreateResponse() { Status = true, Message = $"Created {events.Count()} hook(s)" };
            }
            else if (!string.IsNullOrEmpty(request.ActionName))
            {
                var events = _eventRepository.FindMany(x => x.Action.Name == request.ActionName && (x.SenderId == null || x.SenderId == sender.Id));

                foreach (var item in events)
                {
                    var entity = _hookRepository.Insert(new HookEntity()
                    {
                        EventId = item.Id,
                        SenderId = sender.Id,
                        Data = request.Data,
                        StatusId = (int)StatusEnum.Created,
                        Message = StatusEnum.Created.ToString()
                    });

                    _requesterQueue.Add(entity.Id);
                }

                return new HookCreateResponse() { Status = true, Message = $"Created {events.Count()} hook(s)" };
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<HookDeleteResponse> DeleteHookAsync(HookDeleteRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _hookRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            _hookRepository.Remove(entity);
            return new HookDeleteResponse();
        }
    }
}