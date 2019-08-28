using System.Threading.Tasks;
using OK.Hookman.Core.Requests.Action;
using OK.Hookman.Core.Requests.Event;
using OK.Hookman.Core.Requests.Hook;
using OK.Hookman.Core.Requests.Receiver;
using OK.Hookman.Core.Requests.Sender;
using OK.Hookman.Core.Responses.Action;
using OK.Hookman.Core.Responses.Event;
using OK.Hookman.Core.Responses.Hook;
using OK.Hookman.Core.Responses.Receiver;
using OK.Hookman.Core.Responses.Sender;
using OK.Hookman.Core.Responses.Status;
using Refit;

namespace OK.Hookman.Client
{
    public interface IHookmanClient
    {
        [Get("/actions")]
        Task<ActionListResponse> GetActionsAsync(int pageSize, int pageNumber);

        [Get("/actions/{id}")]
        Task<ActionDetailResponse> GetActionAsync(int id);

        [Post("/actions")]
        Task<ActionCreateResponse> CreateActionAsync([Body] ActionCreateRequest request);

        [Put("/actions/{id}")]
        Task<ActionEditResponse> EditActionAsync(int id, [Body] ActionEditRequest request);

        [Delete("/actions/{id}")]
        Task<ActionDeleteResponse> DeleteActionAsync(int id);


        [Get("/senders")]
        Task<SenderListResponse> GetSendersAsync(int pageSize, int pageNumber);

        [Get("/senders/{id}")]
        Task<SenderDetailResponse> GetSenderAsync(int id);

        [Post("/senders")]
        Task<SenderCreateResponse> CreateSenderAsync([Body] SenderCreateRequest request);

        [Put("/senders/{id}")]
        Task<SenderEditResponse> EditSenderAsync([Query] int id, [Body] SenderEditRequest request);

        [Delete("/senders/{id}")]
        Task<SenderDeleteResponse> DeleteSenderAsync(int id);


        [Get("/receivers")]
        Task<ReceiverListResponse> GetReceiversAsync(int pageSize, int pageNumber);

        [Get("/receivers/{id}")]
        Task<ReceiverDetailResponse> GetReceiverAsync(int id);

        [Post("/receivers")]
        Task<ReceiverCreateResponse> CreateReceiverAsync([Body] ReceiverCreateRequest request);

        [Put("/receivers/{id}")]
        Task<ReceiverEditResponse> EditReceiverAsync([Query] int id, [Body] ReceiverEditRequest request);

        [Delete("/receivers/{id}")]
        Task<ReceiverDeleteResponse> DeleteReceiverAsync(int id);


        [Get("/events")]
        Task<EventListResponse> GetEventsAsync(int pageSize, int pageNumber);

        [Get("/events/{id}")]
        Task<EventDetailResponse> GetEventAsync(int id);

        [Post("/events")]
        Task<EventCreateResponse> CreateEventAsync([Body] EventCreateRequest request);

        [Put("/events/{id}")]
        Task<EventEditResponse> EditEventAsync(int id, [Body] EventEditRequest request);

        [Delete("/events/{id}")]
        Task<EventDeleteResponse> DeleteEventAsync(int id);


        [Get("/hooks")]
        Task<HookListResponse> GetHooksAsync(int pageSize, int pageNumber);

        [Get("/hooks/{id}")]
        Task<HookDetailResponse> GetHookAsync(int id);

        [Post("/hooks")]
        Task<HookCreateResponse> CreateHookAsync([Body] HookCreateRequest request);

        [Delete("/hooks/{id}")]
        Task<HookDeleteResponse> DeleteHookAsync(int id);


        [Get("/statuses")]
        Task<StatusListResponse> GetStatusesAsync(int pageSize, int pageNumber);
    }
}