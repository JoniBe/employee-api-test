using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Aptude.Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Aptude.Core.Base
{
    public abstract class RepositoryBase<TEntity, U> : IDataRepository<TEntity>
        where TEntity : class, new()
        where U : DbContext
    {
        protected readonly U _Context;
        private readonly DbSet<TEntity> _DbSet;

        protected RepositoryBase(U context)
        {
            _Context = context;
            _DbSet = _Context.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            _Context.Set<TEntity>().Add(entity);

            _Context.SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _Context.Set<TEntity>().AddAsync(entity);

            await _Context.SaveChangesAsync();

            return entity;
        }

        public virtual void Remove(TEntity entity)
        {
            _DbSet.Attach(entity);
            _Context.Entry<TEntity>(entity).State = EntityState.Deleted;

            _Context.SaveChanges();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            _DbSet.Attach(entity);

            _Context.Entry<TEntity>(entity).State = EntityState.Deleted;

            await _Context.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            _DbSet.Attach(entity);
            _Context.Entry<TEntity>(entity).State = EntityState.Modified;

            _Context.SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _DbSet.Attach(entity);
            _Context.Entry<TEntity>(entity).State = EntityState.Modified;

            await _Context.SaveChangesAsync();

            return entity;
        }

        public virtual TResult GetSingle<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            return notSortedResults.FirstOrDefault();
        }

        public virtual async Task<TResult> GetSingleAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            return await notSortedResults.FirstOrDefaultAsync();
        }

        public virtual async Task<TResult> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            TResult result = await Task.Run(async () =>
            {
                var query = filter == null ? _DbSet : _DbSet.Where(filter);

                var notSortedResults = transform(query);

                return await notSortedResults.FirstOrDefaultAsync();
            });

            return result;
        }
    }
}
