using System.Threading.Tasks;
using OK.Hookman.Core.Requests.Sender;
using OK.Hookman.Core.Responses.Sender;

namespace OK.Hookman.Service.Abstractions
{
    public interface ISenderService
    {
        Task<SenderListResponse> GetSendersAsync(SenderListRequest request);
        Task<SenderDetailResponse> GetSenderAsync(SenderDetailRequest request);
        Task<SenderCreateResponse> CreateSenderAsync(SenderCreateRequest request);
        Task<SenderEditResponse> EditSenderAsync(SenderEditRequest request);
        Task<SenderDeleteResponse> DeleteSenderAsync(SenderDeleteRequest request);
    }
}