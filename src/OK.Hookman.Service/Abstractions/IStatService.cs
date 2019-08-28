using System.Threading.Tasks;
using OK.Hookman.Core.Requests.Stat;
using OK.Hookman.Core.Responses.Stat;

namespace OK.Hookman.Service.Abstractions
{
    public interface IStatService
    {
        Task<StatTopActionListResponse> GetTopActionListAsync(StatTopActionListRequest request);
    }
}