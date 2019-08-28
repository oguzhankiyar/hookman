using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Persistence.SqlServer.Contexts;

namespace OK.Hookman.Persistence.SqlServer.Repositories
{
    public class HookRepository : BaseRepository<HookEntity>, IHookRepository
    {
        public HookRepository(HookmanDataContext dataContext)
            : base(dataContext, x => x.Event, x => x.Event.Sender, x => x.Event.Receiver, x => x.Event.Action, x => x.Sender, x => x.Status)
        {
        }
    }
}