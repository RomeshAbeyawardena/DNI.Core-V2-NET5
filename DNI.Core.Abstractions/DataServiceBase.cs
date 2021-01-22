using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    /// <inheritdoc cref="IDataService{TEntity}"/>
    public abstract class DataServiceBase<TEntity> : IDataService<TEntity>
        where TEntity : class
    {
        public abstract Task<int> SaveAsync(TEntity entity, CancellationToken cancellationToken);
        public abstract Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Repository.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return Repository.AnyAsync(NoTrackingQuery, expression, cancellationToken);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return Repository.FirstOrDefaultAsync(NoTrackingQuery, expression, cancellationToken);
        }

        public Task<IEnumerable<TEntity>> ToArrayAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return Repository.ToArrayAsync(NoTrackingQuery, expression, cancellationToken);
        }

        public Task<IEnumerable<TEntity>> ToArrayAsync(CancellationToken cancellationToken)
        {
            return Repository.ToArrayAsync(NoTrackingQuery, cancellationToken: cancellationToken);
        }

        public Task<IEnumerable<TEntity>> ToArrayAsync(Expression<Func<TEntity, bool>> expression, IPagingCriteria pagingCriteria, CancellationToken cancellationToken)
        {
            var pager = new Pager<TEntity>(NoTrackingQuery.Where(expression));

            return pager.GetPagedItemsAsync(pagingCriteria.PageIndex, pagingCriteria.TotalItemsPerPage, cancellationToken);
        }

        public void Encrypt(TEntity model)
        {
            ModelEncryptionService.Encrypt(model);
        }

        public void Decrypt(TEntity model)
        {
            ModelEncryptionService.Decrypt(model);
        }

        public TEntity Encrypt(TEntity model, params object[] args)
        {
            return ModelEncryptionService.EncryptAsClone(model, args);
        }

        public TEntity Decrypt(TEntity model, params object[] args)
        {
            return ModelEncryptionService.DecryptAsClone(model, args);
        }

        public TEntity Add(TEntity entity)
        {
            Repository.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Repository.Update(entity);
            return entity;
        }

        protected DataServiceBase(IAsyncRepository<TEntity> entityRepository,
            IModelEncryptionService<TEntity> modelEncryptionService)
        {
            Repository = entityRepository;
            ModelEncryptionService = modelEncryptionService;
        }

        protected IQueryable<TEntity> Query => Repository.Query;
        protected IQueryable<TEntity> NoTrackingQuery => Repository.EnableTracking(Query, false, false);
        protected IAsyncRepository<TEntity> Repository { get; }
        protected IModelEncryptionService<TEntity> ModelEncryptionService { get; }
    }
}
