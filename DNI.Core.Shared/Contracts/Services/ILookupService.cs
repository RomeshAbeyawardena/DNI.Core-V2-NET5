using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface ILookupService
    {
        Type EntityType { get; }
        object Lookup(object parameters);
        Task<object> LookupAsync(object parameters, CancellationToken cancellationToken);
    }

    public interface ILookupService<TLookupParameters, TEntity> : ILookupService
        where TLookupParameters : class, ILookupParameters
    {
        TEntity Lookup(TLookupParameters parameters);
        Task<TEntity> LookupAsync(TLookupParameters parameters, CancellationToken cancellationToken);
    }
}
