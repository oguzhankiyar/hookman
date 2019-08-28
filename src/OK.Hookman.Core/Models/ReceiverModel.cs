using System;
using System.Collections.Generic;

namespace OK.Hookman.Core.Models
{
    public class ReceiverModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public IDictionary<string, string> QueryStrings { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}