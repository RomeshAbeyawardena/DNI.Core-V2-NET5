using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    public interface IDefinition<T> : IEnumerable<T>
    {
        IEnumerable<T> Items { get; }
        IDefinition<T> Add(T item);
        IDefinition<T> AddRange(IEnumerable<T> items);
    }
}
