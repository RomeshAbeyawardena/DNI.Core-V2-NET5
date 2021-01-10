using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class LookupServiceBase<TLookupParameters, TEntity> : ILookupService<TLookupParameters, TEntity>
        where TLookupParameters : class, ILookupParameters
    {
        Type ILookupService.EntityType => typeof(TEntity);

        public abstract TEntity Lookup(TLookupParameters parameters);
        public abstract Task<TEntity> LookupAsync(TLookupParameters parameters, CancellationToken cancellationToken);

        object ILookupService.Lookup(object parameters)
        {
            return Lookup(parameters as TLookupParameters);
        }

        async Task<object> ILookupService.LookupAsync(object parameters, CancellationToken cancellationToken)
        {
            return await LookupAsync(parameters as TLookupParameters, cancellationToken);
        }
    }
}
