using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OK.Hookman.Core.Exceptions;
using OK.Hookman.Core.Requests.Action;
using OK.Hookman.Core.Responses.Action;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Service.Abstractions;
using OK.Hookman.Service.Base;

namespace OK.Hookman.Service.Implementations
{
    public class ActionService : BaseService, IActionService
    {
        private readonly IActionRepository _actionRepository;
        private readonly IMapper _mapper;

        public ActionService(IActionRepository actionRepository, IMapper mapper, IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            _actionRepository = actionRepository ?? throw new ArgumentNullException(nameof(actionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ActionListResponse> GetActionsAsync(ActionListRequest request)
        {
            await ValidateRequestAsync(request);

            var skip = request.PageSize * (request.PageNumber - 1);
            var take = request.PageSize;

            var entities = _actionRepository.FindAll(skip, take).ToList();
            var recordCount = _actionRepository.Count();

            var response = _mapper.Map<ActionListResponse>(entities);
            response.PageSize = request.PageSize;
            response.PageNumber = request.PageNumber;
            response.RecordCount = recordCount;
            response.PageCount = (int)Math.Ceiling((double)response.RecordCount / response.PageSize);
            return response;
        }

        public async Task<ActionDetailResponse> GetActionAsync(ActionDetailRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _actionRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<ActionDetailResponse>(entity);
        }

        public async Task<ActionCreateResponse> CreateActionAsync(ActionCreateRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _mapper.Map<ActionEntity>(request);
            entity = _actionRepository.Insert(entity);
            return _mapper.Map<ActionCreateResponse>(entity);
        }

        public async Task<ActionEditResponse> EditActionAsync(ActionEditRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _actionRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var newEntity = _mapper.Map<ActionEntity>(request);
            entity.Name = newEntity.Name;
            _actionRepository.Update(entity);
            return _mapper.Map<ActionEditResponse>(entity);
        }

        public async Task<ActionDeleteResponse> DeleteActionAsync(ActionDeleteRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _actionRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            _actionRepository.Remove(entity);
            return new ActionDeleteResponse();
        }
    }
}