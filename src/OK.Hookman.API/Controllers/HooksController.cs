using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OK.Hookman.API.Base;
using OK.Hookman.Core.Requests.Hook;
using OK.Hookman.Core.Responses.Hook;
using OK.Hookman.Service.Abstractions;

namespace OK.Hookman.API.Controllers
{
    public class HooksController : BaseController
    {
        private readonly IHookService _hookService;

        public HooksController(IHookService hookService) =>
            _hookService = hookService ?? throw new ArgumentNullException(nameof(hookService));

        [HttpGet]
        public Task<HookListResponse> GetAsync([FromQuery] HookListRequest request) =>
            _hookService.GetHooksAsync(request);

        [HttpGet("{Id}")]
        public Task<HookDetailResponse> GetAsync([FromRoute] HookDetailRequest request) =>
            _hookService.GetHookAsync(request);

        [HttpPost]
        public Task<HookCreateResponse> PostAsync([FromBody] HookCreateRequest request) =>
            _hookService.CreateHookAsync(request);

        [HttpDelete("{Id}")]
        public Task<HookDeleteResponse> DeleteAsync([FromRoute] HookDeleteRequest request) =>
            _hookService.DeleteHookAsync(request);
    }
}