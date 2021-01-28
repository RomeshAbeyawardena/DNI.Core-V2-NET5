using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        /// <summary>
        /// Encrypts <typeparamref name="TEntity"/> using <see cref="IModelEncryptionService{TEntity}"/>
        /// </summary>
        /// <param name="model"></param>
        void Encrypt(TEntity model);

        /// <summary>
        /// Decrypts <typeparamref name="TEntity"/> using <see cref="IModelEncryptionService{TEntity}"/>
        /// </summary>
        /// <param name="model"></param>
        void Decrypt(TEntity model);

        /// <summary>
        /// Encrypts <paramref name="model"/> using <see cref="IModelEncryptionService{TEntity}"/>
        /// </summary>
        /// <param name="model"></param>
        void Encrypt(IEnumerable<TEntity> model);

        /// <summary>
        /// Decrypts <paramref name="model"/> using <see cref="IModelEncryptionService{TEntity}"/>
        /// </summary>
        /// <param name="model"></param>
        void Decrypt(IEnumerable<TEntity> model);

        /// <summary>
        /// Encrypts <paramref name="model"/> using <see cref="IModelEncryptionService{TEntity}"/> as a new instance of <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        TEntity Encrypt(TEntity model, params object[] args);

        /// <summary>
        /// Decrypts <paramref name="model"/> using <see cref="IModelEncryptionService{TEntity}"/> as a new instance of <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        TEntity Decrypt(TEntity model, params object[] args);

        /// <summary>
        /// Encrypts <paramref name="model"/> using <see cref="IModelEncryptionService{TEntity}"/> as a new instance of <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Encrypt(IEnumerable<TEntity> model, params object[] args);

        /// <summary>
        /// Decrypts <paramref name="model"/> using <see cref="IModelEncryptionService{TEntity}"/> as a new instance of <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Decrypt(IEnumerable<TEntity> model, params object[] args);
        /// <summary>
        /// Retrieves an array based off <paramref name="expression"/>
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pagingCriteria"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeInResults"></param>
        /// <returns>A page list in the form of <see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<TEntity>> ToArrayAsync(
            Expression<Func<TEntity, bool>> expression,
            IPagingCriteria pagingCriteria, 
            CancellationToken cancellationToken,
            Action<IIncludeableQuery<TEntity>> includeInResults = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="includeInResults"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> ToArrayAsync(
            CancellationToken cancellationToken, 
            Action<IIncludeableQuery<TEntity>> includeInResults = default);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeInResults"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> expression, 
            CancellationToken cancellationToken,
            Action<IIncludeableQuery<TEntity>> includeInResults = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeInResults"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> ToArrayAsync(
            Expression<Func<TEntity, bool>> expression, 
            CancellationToken cancellationToken,
            Action<IIncludeableQuery<TEntity>> includeInResults = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Attach(TEntity entity); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void AttachRange(IEnumerable<TEntity> entities);
    }
}
