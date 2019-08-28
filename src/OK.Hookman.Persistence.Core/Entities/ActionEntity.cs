using System.Collections.Generic;

namespace OK.Hookman.Persistence.Core.Entities
{
    public class ActionEntity : BaseEntity
    {
        public string Name { get; set; }
        
        public virtual ICollection<EventEntity> Events { get; set; }
    }
}