using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface ILookupService<TKey, TEntity, TLookupParameters> : ILookup<TKey, TEntity>
        where TLookupParameters : ILookupParameters
    {
        TEntity Lookup(TLookupParameters parameters);
        Task<TEntity> LookupAsync(TLookupParameters parameters, CancellationToken cancellationToken);
    }
}
