using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected DataServiceBase(IAsyncRepository<TEntity> entityRepository)
        {
            Repository = entityRepository;
        }

        protected IQueryable<TEntity> NoTrackingQuery => Repository.EnableTracking(Repository.Query);
        protected IAsyncRepository<TEntity> Repository { get; }
    }
}
