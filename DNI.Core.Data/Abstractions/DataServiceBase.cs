using DNI.Core.Data.Extensions;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Data.Abstractions
{
     public abstract class DataServiceBase<TEntity> : Core.Abstractions.DataServiceBase<TEntity>
        where TEntity: class
    {
        public override Task<int> SaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return Repository.AddOrUpdate(identityKey, entity, cancellationToken);
        }

        protected DataServiceBase(IAsyncRepository<TEntity> repository, 
            IModelEncryptionService<TEntity> modelEncryptionService,
            Func<TEntity, object> identityKey)
            : base(repository, modelEncryptionService)
        {
            this.identityKey = identityKey;
        }
        
        private readonly Func<TEntity, object> identityKey;
    }
}
