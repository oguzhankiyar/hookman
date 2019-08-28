using System.Threading.Tasks;
using OK.Hookman.Core.Requests.Hook;
using OK.Hookman.Core.Responses.Hook;

namespace OK.Hookman.Service.Abstractions
{
    public interface IHookService
    {
        Task<HookListResponse> GetHooksAsync(HookListRequest request);
        Task<HookDetailResponse> GetHookAsync(HookDetailRequest request);
        Task<HookCreateResponse> CreateHookAsync(HookCreateRequest request);
        Task<HookDeleteResponse> DeleteHookAsync(HookDeleteRequest request);
    }
}