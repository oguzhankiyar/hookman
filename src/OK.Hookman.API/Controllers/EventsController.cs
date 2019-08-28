using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OK.Hookman.API.Base;
using OK.Hookman.Core.Requests.Event;
using OK.Hookman.Core.Responses.Event;
using OK.Hookman.Service.Abstractions;

namespace OK.Hookman.API.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService) =>
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));

        [HttpGet]
        public Task<EventListResponse> GetAsync([FromQuery] EventListRequest request) =>
            _eventService.GetEventsAsync(request);

        [HttpGet("{Id}")]
        public Task<EventDetailResponse> GetAsync([FromRoute] EventDetailRequest request) =>
            _eventService.GetEventAsync(request);

        [HttpPost]
        public Task<EventCreateResponse> PostAsync([FromBody] EventCreateRequest request) =>
            _eventService.CreateEventAsync(request);

        [HttpPut("{Id}")]
        public Task<EventEditResponse> PutAsync([FromRoute] int id, [FromBody] EventEditRequest request) =>
            _eventService.EditEventAsync(request);

        [HttpDelete("{Id}")]
        public Task<EventDeleteResponse> DeleteAsync([FromRoute] EventDeleteRequest request) =>
            _eventService.DeleteEventAsync(request);
    }
}