using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Persistence.SqlServer.Contexts;

namespace OK.Hookman.Persistence.SqlServer.Repositories
{
    public class EventRepository : BaseRepository<EventEntity>, IEventRepository
    {
        public EventRepository(HookmanDataContext dataContext)
            : base(dataContext, x => x.Sender, x => x.Receiver, x => x.Action)
        {
        }
    }
}