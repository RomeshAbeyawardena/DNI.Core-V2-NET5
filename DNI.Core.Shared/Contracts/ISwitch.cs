using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    public interface ISwitch<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        new ISwitch<TKey, TValue> Add(KeyValuePair<TKey, TValue> item);
        new ISwitch<TKey, TValue> Add(TKey key, TValue value);
    }
}
