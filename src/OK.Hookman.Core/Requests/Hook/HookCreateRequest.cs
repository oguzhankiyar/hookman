namespace OK.Hookman.Core.Requests.Hook
{
    public class HookCreateRequest
    {
        public int? EventId { get; set; }
        public string SenderToken { get; set; }
        public int? ActionId { get; set; }
        public string ActionName { get; set; }
        public string Data { get; set; }
    }
}