using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface ICacheDependency
    {
        ICacheDependencyOptions Options { get; }
        Task<bool> Verify(string key, CancellationToken cancellationToken);
        Task Update(string key, CancellationToken cancellationToken);
    }
}
