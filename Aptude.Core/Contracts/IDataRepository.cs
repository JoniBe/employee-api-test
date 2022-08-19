using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aptude.Core.Contracts
{
    public interface IDataRepository
    { }

    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        T Add(T entity);

        Task<T> AddAsync(T entity);

        void Remove(T entity);

        Task RemoveAsync(T entity);

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        TResult GetSingle<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

        Task<TResult> GetSingleAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

        Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);
    }
}
