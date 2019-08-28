using System;
using System.Collections.Generic;
using OK.Hookman.Core.Enums;

namespace OK.Hookman.Core.Models
{
    public class HookModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public StatusEnum StatusId { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
        public string RequestUrl { get; set; }
        public IDictionary<string, string> RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public int? ResponseCode { get; set; }
        public IDictionary<string, string> ResponseHeaders { get; set; }
        public string ResponseBody { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public EventModel Event { get; set; }
        public SenderModel Sender { get; set; }
        public StatusModel Status { get; set; }
    }
}