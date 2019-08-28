using System.Collections.Generic;

namespace OK.Hookman.Persistence.Core.Entities
{
    public class EventEntity : BaseEntity
    {
        public int? SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ActionId { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string QueryStrings { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public int RetryCount { get; set; }

        public virtual SenderEntity Sender { get; set; }
        public virtual ReceiverEntity Receiver { get; set; }
        public virtual ActionEntity Action { get; set; }
        public virtual ICollection<HookEntity> Hooks { get; set; }
    }
}