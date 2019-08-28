using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.Core.Repositories;
using OK.Hookman.Persistence.SqlServer.Contexts;

namespace OK.Hookman.Persistence.SqlServer.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly HookmanDataContext _dataContext;
        protected Expression<Func<TEntity, object>>[] _queryIncludes;

        protected DbSet<TEntity> Entities
        {
            get
            {
                return _dataContext.Set<TEntity>();
            }
        }

        protected BaseRepository(HookmanDataContext dataContext, params Expression<Func<TEntity, object>>[] queryIncludes)
        {
            _dataContext = dataContext;
            _queryIncludes = queryIncludes;
        }

        public virtual IQueryable<TEntity> FindAll(int? skip, int? take)
        {
            var query = Entities.Where(x => x.IsDeleted == false);

            foreach (var queryInclude in _queryIncludes)
            {
                query = query.Include(queryInclude);
            }

            if (skip.HasValue && take.HasValue)
            {
                query = query.Skip(skip.Value).Take(take.Value);
            }

            return query;
        }

        public virtual IQueryable<TEntity> FindMany(Expression<Func<TEntity, bool>> predicate)
        {
            var query = Entities.Where(x => x.IsDeleted == false);
            
            foreach (var queryInclude in _queryIncludes)
            {
                query = query.Include(queryInclude);
            }

            return query.Where(predicate);
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            var query = Entities.Where(x => x.IsDeleted == false);
            
            foreach (var queryInclude in _queryIncludes)
            {
                query = query.Include(queryInclude);
            }

            return query.FirstOrDefault(predicate);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.CreatedDate = DateTime.UtcNow;

            _dataContext.Entry(entity).State = EntityState.Added;
            _dataContext.SaveChanges();

            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;

            _dataContext.Entry(entity).State = EntityState.Modified;

            _dataContext.SaveChanges();
        }

        public virtual void Remove(int id)
        {
            TEntity entity = FindOne(x => x.Id == id);

            Remove(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;

            _dataContext.Entry(entity).State = EntityState.Modified;

            _dataContext.SaveChanges();
        }

        public virtual int Count()
        {
            return Entities.Count(x => x.IsDeleted == false);
        }
    }
}