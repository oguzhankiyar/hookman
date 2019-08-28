using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OK.Hookman.API.Base;
using OK.Hookman.Core.Requests.Receiver;
using OK.Hookman.Core.Responses.Receiver;
using OK.Hookman.Service.Abstractions;

namespace OK.Hookman.API.Controllers
{
    public class ReceiversController : BaseController
    {
        private readonly IReceiverService _receiverService;

        public ReceiversController(IReceiverService receiverService) =>
            _receiverService = receiverService ?? throw new ArgumentNullException(nameof(receiverService));

        [HttpGet]
        public Task<ReceiverListResponse> GetAsync([FromQuery] ReceiverListRequest request) =>
            _receiverService.GetReceiversAsync(request);

        [HttpGet("{Id}")]
        public Task<ReceiverDetailResponse> GetAsync([FromRoute] ReceiverDetailRequest request) =>
            _receiverService.GetReceiverAsync(request);

        [HttpPost]
        public Task<ReceiverCreateResponse> PostAsync([FromBody] ReceiverCreateRequest request) =>
            _receiverService.CreateReceiverAsync(request);

        [HttpPut("{Id}")]
        public Task<ReceiverEditResponse> PutAsync([FromRoute] int id, [FromBody] ReceiverEditRequest request) =>
            _receiverService.EditReceiverAsync(request);

        [HttpDelete("{Id}")]
        public Task<ReceiverDeleteResponse> DeleteAsync([FromRoute] ReceiverDeleteRequest request) =>
            _receiverService.DeleteReceiverAsync(request);
    }
}