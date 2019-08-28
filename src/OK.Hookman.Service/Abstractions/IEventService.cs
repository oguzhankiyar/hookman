using System.Threading.Tasks;
using OK.Hookman.Core.Requests.Event;
using OK.Hookman.Core.Responses.Event;

namespace OK.Hookman.Service.Abstractions
{
    public interface IEventService
    {
        Task<EventListResponse> GetEventsAsync(EventListRequest request);
        Task<EventDetailResponse> GetEventAsync(EventDetailRequest request);
        Task<EventCreateResponse> CreateEventAsync(EventCreateRequest request);
        Task<EventEditResponse> EditEventAsync(EventEditRequest request);
        Task<EventDeleteResponse> DeleteEventAsync(EventDeleteRequest request);
    }
}