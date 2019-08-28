using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Persistence.SqlServer.Contexts;

namespace OK.Hookman.Persistence.SqlServer.Repositories
{
    public class StatusRepository : BaseRepository<StatusEntity>, IStatusRepository
    {
        public StatusRepository(HookmanDataContext dataContext) : base(dataContext)
        {
        }
    }
}