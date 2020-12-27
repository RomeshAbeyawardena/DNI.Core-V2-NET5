using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    internal class DictionaryBuilder<TKey, TValue> : IDictionaryBuilder<TKey, TValue>
    {
        public IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return Add(keyValuePair.Key, keyValuePair.Value);
        }

        public IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value)
        {
            dictionary.TryAdd(key, value);
            return this;
        }

        public IDictionaryBuilder<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            keyValuePairs.ForEach(keyValuePair => Add(keyValuePair));
            return this;
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            Add(key, value);
        }

        bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            return dictionary.TryRemove(key, out var value);
        }

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            dictionary.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            var successful = dictionary.TryRemove(item.Key, out var item1);

            if(successful)
            { 
                Removed?.Invoke(item);
            }

            return successful;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => dictionary.Keys;

        ICollection<TValue> IDictionary<TKey, TValue>.Values => dictionary.Values;

        int ICollection<KeyValuePair<TKey, TValue>>.Count => dictionary.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        TValue IDictionary<TKey, TValue>.this[TKey key] { 
            get { dictionary.TryGetValue(key, out var value); return value; } 
            set => Add(key, value); 
        }

        internal DictionaryBuilder()
        {
            dictionary = new ConcurrentDictionary<TKey, TValue>();
        }

        internal DictionaryBuilder(Action<IDictionaryBuilder<TKey, TValue>> buildAction)
            : this()
        {
            buildAction(this);
        }

        private readonly ConcurrentDictionary<TKey, TValue> dictionary;

        public event IDictionaryBuilder<TKey, TValue>.KeyValuePairChanged Removed;
    }
}
