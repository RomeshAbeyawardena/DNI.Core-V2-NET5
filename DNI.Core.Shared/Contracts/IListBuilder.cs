using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    public interface IListBuilder<T> : IEnumerable<T>, IReadOnlyList<T>
    {
        IListBuilder<T> Add(T item);
        IListBuilder<T> AddRange(IEnumerable<T> item);
    }
}
