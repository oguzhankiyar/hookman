using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OK.Hookman.Core.Exceptions;
using OK.Hookman.Core.Requests.Sender;
using OK.Hookman.Core.Responses.Sender;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Service.Abstractions;
using OK.Hookman.Service.Base;

namespace OK.Hookman.Service.Implementations
{
    public class SenderService : BaseService, ISenderService
    {
        private readonly ISenderRepository _senderRepository;
        private readonly IMapper _mapper;

        public SenderService(ISenderRepository senderRepository, IMapper mapper, IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            _senderRepository = senderRepository ?? throw new ArgumentNullException(nameof(senderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SenderListResponse> GetSendersAsync(SenderListRequest request)
        {
            await ValidateRequestAsync(request);

            var skip = request.PageSize * (request.PageNumber - 1);
            var take = request.PageSize;

            var entities = _senderRepository.FindAll(skip, take).ToList();
            var recordCount = _senderRepository.Count();

            var response = _mapper.Map<SenderListResponse>(entities);
            response.PageSize = request.PageSize;
            response.PageNumber = request.PageNumber;
            response.RecordCount = recordCount;
            response.PageCount = (int)Math.Ceiling((double)response.RecordCount / response.PageSize);
            return response;
        }

        public async Task<SenderDetailResponse> GetSenderAsync(SenderDetailRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _senderRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<SenderDetailResponse>(entity);
        }

        public async Task<SenderCreateResponse> CreateSenderAsync(SenderCreateRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _mapper.Map<SenderEntity>(request);
            entity.Token = Guid.NewGuid().ToString("N");
            entity = _senderRepository.Insert(entity);
            return _mapper.Map<SenderCreateResponse>(entity);
        }

        public async Task<SenderEditResponse> EditSenderAsync(SenderEditRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _senderRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            
            var newEntity = _mapper.Map<SenderEntity>(request);
            entity.Name = newEntity.Name;
            _senderRepository.Update(entity);
            return _mapper.Map<SenderEditResponse>(entity);
        }

        public async Task<SenderDeleteResponse> DeleteSenderAsync(SenderDeleteRequest request)
        {
            await ValidateRequestAsync(request);

            var entity = _senderRepository.FindOne(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }
            _senderRepository.Remove(entity);
            return new SenderDeleteResponse();
        }
    }
}