using System.Collections.Generic;

namespace OK.Hookman.Core.Requests.Receiver
{
    public class ReceiverCreateRequest
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public IDictionary<string, string> QueryStrings { get; set; }
    }
}