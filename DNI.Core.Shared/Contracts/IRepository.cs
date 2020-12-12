using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// Represents a repository pattern for data source access of <typeparamref name="T"/> entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Returns a <see cref="IQueryable{T}"/> object to query the data source
        /// </summary>
        IQueryable<T> Query { get; }
        /// <summary>
        /// Enables or disables change tracking on the <paramref name="query"/> 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="enableTracking">Sets whether change tracking should be enabled</param>
        /// <param name="enableIdentityResolution">Sets whether identity resolution should be enabled</param>
        /// <returns></returns>
        IQueryable<T> EnableTracking(IQueryable<T> query, bool enableTracking = true, bool enableIdentityResolution = true);

        /// <summary>
        /// Finds an entity in the data source with the specified unique keys 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>An entity of <typeparamref name="T"/></returns>
        T Find(params object[] keys);

        /// <summary>
        /// Commit changes to data source
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Marks entity to be added to data source change tracker
        /// </summary>
        /// <param name="result"></param>
        void Add(T result);

        /// <summary>
        /// Marks entity to be updated to data source change tracker
        /// </summary>
        /// <param name="result"></param>
        void Update(T result);

        /// <summary>
        /// Marks entity to be removed from the data source change tracker
        /// </summary>
        /// <param name="result"></param>
        void Remove(T result);
    }

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
