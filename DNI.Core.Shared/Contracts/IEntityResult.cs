using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IEntityResult<TEntity>
    {
        TEntity Result { get; }
        int AffectedRows { get; }
        IDictionary<string, string> Properties { get; }
    }
}
