using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Persistence.SqlServer.Contexts;

namespace OK.Hookman.Persistence.SqlServer.Repositories
{
    public class SenderRepository : BaseRepository<SenderEntity>, ISenderRepository
    {
        public SenderRepository(HookmanDataContext dataContext)
            : base(dataContext, x => x.Events)
        {
        }
    }
}