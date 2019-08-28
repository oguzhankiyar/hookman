using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OK.Hookman.API.Base;
using OK.Hookman.Core.Requests.Status;
using OK.Hookman.Core.Responses.Status;
using OK.Hookman.Service.Abstractions;

namespace OK.Hookman.API.Controllers
{
    public class StatusesController : BaseController
    {
        private readonly IStatusService _statusService;

        public StatusesController(IStatusService statusService) =>
            _statusService = statusService ?? throw new ArgumentNullException(nameof(statusService));

        [HttpGet]
        public Task<StatusListResponse> GetAsync([FromQuery] StatusListRequest request) =>
            _statusService.GetStatusesAsync(request);
    }
}