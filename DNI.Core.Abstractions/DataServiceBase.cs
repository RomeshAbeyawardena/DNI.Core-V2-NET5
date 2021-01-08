using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class DataServiceBase<TEntity> : IDataService<TEntity>
        where TEntity : class
    {
        public abstract Task<int> Save(TEntity entity, CancellationToken cancellationToken);

        Task<int> IDataService<TEntity>.SaveChanges(CancellationToken cancellationToken)
        {
            return Repository.SaveChangesAsync(cancellationToken);
        }

        Task<TEntity> IDataService<TEntity>.FirstOrDefault(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return Repository.FirstOrDefaultAsync(NoTrackingQuery, expression, cancellationToken);
        }

        Task<IEnumerable<TEntity>> IDataService<TEntity>.ToArray(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return Repository.ToArrayAsync(NoTrackingQuery, expression, cancellationToken);
        }

        Task<IEnumerable<TEntity>> IDataService<TEntity>.ToArray(CancellationToken cancellationToken)
        {
            return Repository.ToArrayAsync(cancellationToken: cancellationToken);
        }

        protected DataServiceBase(IAsyncRepository<TEntity> entityRepository)
        {
            Repository = entityRepository;
        }

        protected IQueryable<TEntity> NoTrackingQuery => Repository.EnableTracking(Repository.Query);
        protected IAsyncRepository<TEntity> Repository { get; }
    }
}
