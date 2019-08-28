using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OK.Hookman.Core.Exceptions;
using OK.Hookman.Core.Requests.Event;
using OK.Hookman.Core.Responses.Event;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Service.Abstractions;
using OK.Hookman.Service.Base;

namespace OK.Hookman.Service.Implementations
{
    public class EventService : BaseService, IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper, IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<EventListResponse> GetEventsAsync(EventListRequest request)
        {
            await ValidateRequestAsync(request);

            var skip = request.PageSize * (request.PageNumber - 1);
            var take = request.PageSize;

            var entities = _eventRepository.FindAll(skip, take).ToList();
            var recordCount = _eventRepository.Count();

            var response = _mapper.Map<EventListResponse>(entities);
            response.PageSize = request.PageSize;
            response.PageNumber = request.PageNumber;
            response.RecordCount = recordCount;
            response.PageCount = (int)Math.Ceiling((double)response.RecordCount / response.PageSize);
            return response;
        }

        public async Task<EventDetailResponse> GetEventAsync(EventDetailRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _eventRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<EventDetailResponse>(entity);
        }

        public async Task<EventCreateResponse> CreateEventAsync(EventCreateRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _mapper.Map<EventEntity>(request);
            entity = _eventRepository.Insert(entity);
            return _mapper.Map<EventCreateResponse>(entity);
        }

        public async Task<EventEditResponse> EditEventAsync(EventEditRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _eventRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var newEntity = _mapper.Map<EventEntity>(request);
            entity.SenderId = newEntity.SenderId;
            entity.ReceiverId = newEntity.ReceiverId;
            entity.ActionId = newEntity.ActionId;
            entity.Method = newEntity.Method;
            entity.Path = newEntity.Path;
            entity.QueryStrings = newEntity.QueryStrings;
            entity.Headers = newEntity.Headers;
            entity.Body = newEntity.Body;
            entity.RetryCount = newEntity.RetryCount;
            _eventRepository.Update(entity);
            return _mapper.Map<EventEditResponse>(entity);
        }

        public async Task<EventDeleteResponse> DeleteEventAsync(EventDeleteRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _eventRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            _eventRepository.Remove(entity);
            return new EventDeleteResponse();
        }
    }
}