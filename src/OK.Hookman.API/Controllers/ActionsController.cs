using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OK.Hookman.API.Base;
using OK.Hookman.Core.Requests.Action;
using OK.Hookman.Core.Responses.Action;
using OK.Hookman.Service.Abstractions;

namespace OK.Hookman.API.Controllers
{
    public class ActionsController : BaseController
    {
        private readonly IActionService _actionService;

        public ActionsController(IActionService actionService) =>
            _actionService = actionService ?? throw new ArgumentNullException(nameof(actionService));

        [HttpGet]
        public Task<ActionListResponse> GetAsync([FromQuery] ActionListRequest request) =>
            _actionService.GetActionsAsync(request);

        [HttpGet("{Id}")]
        public Task<ActionDetailResponse> GetAsync([FromRoute] ActionDetailRequest request) =>
            _actionService.GetActionAsync(request);

        [HttpPost]
        public Task<ActionCreateResponse> PostAsync([FromBody] ActionCreateRequest request) =>
            _actionService.CreateActionAsync(request);

        [HttpPut("{Id}")]
        public Task<ActionEditResponse> PutAsync([FromRoute] int id, [FromBody] ActionEditRequest request) =>
            _actionService.EditActionAsync(request);

        [HttpDelete("{Id}")]
        public Task<ActionDeleteResponse> DeleteAsync([FromRoute] ActionDeleteRequest request) =>
            _actionService.DeleteActionAsync(request);
    }
}