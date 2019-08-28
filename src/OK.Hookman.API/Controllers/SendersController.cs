using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OK.Hookman.API.Base;
using OK.Hookman.Core.Requests.Sender;
using OK.Hookman.Core.Responses.Sender;
using OK.Hookman.Service.Abstractions;

namespace OK.Hookman.API.Controllers
{
    public class SendersController : BaseController
    {
        private readonly ISenderService _senderService;

        public SendersController(ISenderService senderService) =>
            _senderService = senderService ?? throw new ArgumentNullException(nameof(senderService));

        [HttpGet]
        public Task<SenderListResponse> GetAsync([FromQuery] SenderListRequest request) =>
            _senderService.GetSendersAsync(request);

        [HttpGet("{Id}")]
        public Task<SenderDetailResponse> GetAsync([FromRoute] SenderDetailRequest request) =>
            _senderService.GetSenderAsync(request);

        [HttpPost]
        public Task<SenderCreateResponse> PostAsync([FromBody] SenderCreateRequest request) =>
            _senderService.CreateSenderAsync(request);

        [HttpPut("{Id}")]
        public Task<SenderEditResponse> PutAsync([FromRoute] int id, [FromBody]SenderEditRequest request) =>
            _senderService.EditSenderAsync(request);

        [HttpDelete("{Id}")]
        public Task<SenderDeleteResponse> DeleteAsync([FromRoute] SenderDeleteRequest request) =>
            _senderService.DeleteSenderAsync(request);
    }
}