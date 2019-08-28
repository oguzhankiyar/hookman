using System;
using System.Collections.Generic;

namespace OK.Hookman.Core.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ActionId { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> QueryStrings { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public int RetryCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public SenderModel Sender { get; set; }
        public ReceiverModel Receiver { get; set; }
        public ActionModel Action { get; set; }
    }
}