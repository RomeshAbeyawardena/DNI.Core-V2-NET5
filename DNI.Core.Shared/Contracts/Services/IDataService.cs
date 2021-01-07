using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IDataService<TEntity>
        where TEntity : class
    {
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
