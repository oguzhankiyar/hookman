namespace OK.Hookman.Persistence.Core.Entities
{
    public class HookEntity : BaseEntity
    {
        public int EventId { get; set; }
        public int SenderId { get; set; }
        public int StatusId { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
        public string RequestUrl { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public int? ResponseCode { get; set; }
        public string ResponseBody { get; set; }
        public string ResponseHeaders { get; set; }

        public virtual EventEntity Event { get; set; }
        public virtual SenderEntity Sender { get; set; }
        public virtual StatusEntity Status { get; set; }
    }
}