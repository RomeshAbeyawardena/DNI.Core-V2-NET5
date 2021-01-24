using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    /// <summary>
    /// Implements an abstract data service that supports interfacing with an <see cref="IRepository{T}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataService<TEntity>
        where TEntity : class
    {
        void Encrypt(TEntity model);
        void Decrypt(TEntity model);

        void Encrypt(IEnumerable<TEntity> model);
        void Decrypt(IEnumerable<TEntity> model);

        TEntity Encrypt(TEntity model, params object[] args);
        TEntity Decrypt(TEntity model, params object[] args);

        IEnumerable<TEntity> Encrypt(IEnumerable<TEntity> model, params object[] args);
        IEnumerable<TEntity> Decrypt(IEnumerable<TEntity> model, params object[] args);
        /// <summary>
        /// Retrieves an array based off <paramref name="expression"/>
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pagingCriteria"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A page list in the form of <see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<TEntity>> ToArrayAsync(Expression<Func<TEntity, bool>> expression,
            IPagingCriteria pagingCriteria, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> ToArrayAsync(CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> ToArrayAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveAsync(TEntity entity, CancellationToken cancellationToken);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
