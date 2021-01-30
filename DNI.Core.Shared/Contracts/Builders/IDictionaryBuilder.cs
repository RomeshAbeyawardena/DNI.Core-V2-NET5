using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts.Builders
{
    public interface IDictionaryBuilder<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public delegate void KeyValuePairChanged(KeyValuePair<TKey, TValue> keyValuePair);
        /// <summary>
        /// Adds <paramref name="keyValuePairs"/> to dictionary
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        IDictionaryBuilder<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs);
        new IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValuePair);
        new IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);

        event KeyValuePairChanged Removed;
    }
}
