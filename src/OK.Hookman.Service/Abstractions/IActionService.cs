using System.Threading.Tasks;
using OK.Hookman.Core.Requests.Action;
using OK.Hookman.Core.Responses.Action;

namespace OK.Hookman.Service.Abstractions
{
    public interface IActionService
    {
        Task<ActionListResponse> GetActionsAsync(ActionListRequest request);
        Task<ActionDetailResponse> GetActionAsync(ActionDetailRequest request);
        Task<ActionCreateResponse> CreateActionAsync(ActionCreateRequest request);
        Task<ActionEditResponse> EditActionAsync(ActionEditRequest request);
        Task<ActionDeleteResponse> DeleteActionAsync(ActionDeleteRequest request);
    }
}