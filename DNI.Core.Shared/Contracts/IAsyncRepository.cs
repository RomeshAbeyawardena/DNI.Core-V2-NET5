using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// Represents a repository for database access of <typeparamref name="T"/> entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncRepository<T> : IRepository<T>
    {
        
        /// <summary>
        /// Finds an entity in the data source with the specified unique keys 
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An entity of <see cref="Task{T}"/></returns>
        Task<T> FindAsync(
            CancellationToken? cancellationToken = default, 
            params object[] keys);


        Task<T> FirstOrDefaultAsync(IQueryable<T> query = default,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<T> SingleOrDefaultAsync(IQueryable<T> query = default,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<IEnumerable<T>> ToArrayAsync(IQueryable<T> query = default,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<bool> AnyAsync(IQueryable<T> query,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<int> CountAsync(IQueryable<T> query = default,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        /// <summary>
        /// Commit changes to data source
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null);
    }
}
