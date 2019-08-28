using System.Threading.Tasks;
using OK.Hookman.Core.Requests.Status;
using OK.Hookman.Core.Responses.Status;

namespace OK.Hookman.Service.Abstractions
{
    public interface IStatusService
    {
        Task<StatusListResponse> GetStatusesAsync(StatusListRequest request);
    }
}