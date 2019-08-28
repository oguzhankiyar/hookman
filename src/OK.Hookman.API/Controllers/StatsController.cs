using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OK.Hookman.API.Base;
using OK.Hookman.Core.Requests.Stat;
using OK.Hookman.Core.Responses.Stat;
using OK.Hookman.Service.Abstractions;

namespace OK.Hookman.API.Controllers
{
    public class StatsController : BaseController
    {
        private readonly IStatService _statService;

        public StatsController(IStatService statService) =>
            _statService = statService ?? throw new ArgumentNullException(nameof(statService));

        [HttpGet("topactions")]
        public Task<StatTopActionListResponse> GetTopActionListAsync([FromQuery] StatTopActionListRequest request) =>
            _statService.GetTopActionListAsync(request);
    }
}