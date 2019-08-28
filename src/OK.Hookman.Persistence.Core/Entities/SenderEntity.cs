using System.Collections.Generic;

namespace OK.Hookman.Persistence.Core.Entities
{
    public class SenderEntity : BaseEntity
    {
        public string Token { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EventEntity> Events { get; set; }
        public virtual ICollection<HookEntity> Hooks { get; set; }
    }
}