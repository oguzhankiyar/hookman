using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OK.Hookman.Core.Exceptions;
using OK.Hookman.Core.Requests.Receiver;
using OK.Hookman.Core.Responses.Receiver;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Service.Abstractions;
using OK.Hookman.Service.Base;

namespace OK.Hookman.Service.Implementations
{
    public class ReceiverService : BaseService, IReceiverService
    {
        private readonly IReceiverRepository _receiverRepository;
        private readonly IMapper _mapper;

        public ReceiverService(IReceiverRepository receiverRepository, IMapper mapper, IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            _receiverRepository = receiverRepository ?? throw new ArgumentNullException(nameof(receiverRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ReceiverListResponse> GetReceiversAsync(ReceiverListRequest request)
        {
            await ValidateRequestAsync(request);

            var skip = request.PageSize * (request.PageNumber - 1);
            var take = request.PageSize;

            var entities = _receiverRepository.FindAll(skip, take).ToList();
            var recordCount = _receiverRepository.Count();

            var response = _mapper.Map<ReceiverListResponse>(entities);
            response.PageSize = request.PageSize;
            response.PageNumber = request.PageNumber;
            response.RecordCount = recordCount;
            response.PageCount = (int)Math.Ceiling((double)response.RecordCount / response.PageSize);
            return response;
        }

        public async Task<ReceiverDetailResponse> GetReceiverAsync(ReceiverDetailRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _receiverRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<ReceiverDetailResponse>(entity);
        }

        public async Task<ReceiverCreateResponse> CreateReceiverAsync(ReceiverCreateRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _mapper.Map<ReceiverEntity>(request);
            entity = _receiverRepository.Insert(entity);
            return _mapper.Map<ReceiverCreateResponse>(entity);
        }

        public async Task<ReceiverEditResponse> EditReceiverAsync(ReceiverEditRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _receiverRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var newEntity = _mapper.Map<ReceiverEntity>(request);
            entity.Name = newEntity.Name;
            entity.Url = newEntity.Url;
            entity.Path = newEntity.Path;
            entity.Headers = newEntity.Headers;
            entity.QueryStrings = newEntity.QueryStrings;
            _receiverRepository.Update(entity);
            return _mapper.Map<ReceiverEditResponse>(entity);
        }

        public async Task<ReceiverDeleteResponse> DeleteReceiverAsync(ReceiverDeleteRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _receiverRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            _receiverRepository.Remove(entity);
            return new ReceiverDeleteResponse();
        }
    }
}