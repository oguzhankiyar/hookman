using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OK.Hookman.Core.Requests.Status;
using OK.Hookman.Core.Responses.Status;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Service.Abstractions;
using OK.Hookman.Service.Base;

namespace OK.Hookman.Service.Implementations
{
    public class StatusService : BaseService, IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository statusRepository, IMapper mapper, IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            _statusRepository = statusRepository ?? throw new ArgumentNullException(nameof(statusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<StatusListResponse> GetStatusesAsync(StatusListRequest request)
        {
            await ValidateRequestAsync(request);

            var entities = _statusRepository.FindAll().ToList();
            return _mapper.Map<StatusListResponse>(entities);
        }
    }
}