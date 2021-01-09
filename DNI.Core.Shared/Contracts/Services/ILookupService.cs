using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface ILookupService<TEntity>
    {
        TEntity Lookup(ILookupParameters parameters);
        Task<TEntity> LookupAsync(ILookupParameters parameters);
    }
}
