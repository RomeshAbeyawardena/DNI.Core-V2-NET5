using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    public interface IEntityResult<TEntity> : IAttempt<TEntity>
    {
        int AffectedRows { get; }
        IDictionary<string, string> Properties { get; }
    }
}
