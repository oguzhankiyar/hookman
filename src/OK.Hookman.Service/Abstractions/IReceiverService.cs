using System.Threading.Tasks;
using OK.Hookman.Core.Requests.Receiver;
using OK.Hookman.Core.Responses.Receiver;

namespace OK.Hookman.Service.Abstractions
{
    public interface IReceiverService
    {
        Task<ReceiverListResponse> GetReceiversAsync(ReceiverListRequest request);
        Task<ReceiverDetailResponse> GetReceiverAsync(ReceiverDetailRequest request);
        Task<ReceiverCreateResponse> CreateReceiverAsync(ReceiverCreateRequest request);
        Task<ReceiverEditResponse> EditReceiverAsync(ReceiverEditRequest request);
        Task<ReceiverDeleteResponse> DeleteReceiverAsync(ReceiverDeleteRequest request);
    }
}