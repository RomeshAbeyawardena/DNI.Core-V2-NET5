﻿using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface ILookupService<TLookupParameters, TEntity>
        where TLookupParameters : ILookupParameters
    {
        TEntity Lookup(TLookupParameters parameters);
        Task<TEntity> LookupAsync(TLookupParameters parameters, CancellationToken cancellationToken);
    }
}
