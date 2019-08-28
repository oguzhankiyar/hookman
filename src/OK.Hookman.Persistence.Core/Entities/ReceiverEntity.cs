using System.Collections.Generic;

namespace OK.Hookman.Persistence.Core.Entities
{
    public class ReceiverEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public string Headers { get; set; }
        public string QueryStrings { get; set; }
        public virtual ICollection<EventEntity> Events { get; set; }
    }
}