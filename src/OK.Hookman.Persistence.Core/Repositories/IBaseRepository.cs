using System;
using System.Linq;
using System.Linq.Expressions;

namespace OK.Hookman.Persistence.Core.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        IQueryable<TEntity> FindAll(int? skip = null, int? take = null);
        IQueryable<TEntity> FindMany(Expression<Func<TEntity, bool>> predicate);
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate);
        TEntity Insert(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
        void Remove(TEntity entity);
        int Count();
    }
}