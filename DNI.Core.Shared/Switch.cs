using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DNI.Core.Shared.Contracts;

namespace DNI.Core.Shared
{
    /// <summary>
    /// <inheritdoc cref="ISwitch{TKey, TValue}" />
    /// </summary>
    public static class Switch
    {
        public static ISwitch<TKey, TValue> Create<TKey, TValue>()
        {
            return new DefaultSwitch<TKey, TValue>();
        }

        /// <summary>
        /// Creates an instance of <see cref="ISwitch{TKey, TValue}"/>
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="createSwitchDelegate"></param>
        /// <returns></returns>
        public static ISwitch<TKey, TValue> Create<TKey, TValue>(Action<ISwitch<TKey, TValue>> createSwitchDelegate)
        {
            return new DefaultSwitch<TKey, TValue>(createSwitchDelegate);
        }
    }

    /// <inheritdoc cref="ISwitch{TKey, TValue}"/>
    internal class DefaultSwitch<TKey, TValue> : ISwitch<TKey, TValue>
    {
        public DefaultSwitch()
        {
            dictionary = new ConcurrentDictionary<TKey, TValue>();
        }

        public DefaultSwitch(Action<ISwitch<TKey, TValue>> createSwitchDelegate)
            : this()
        {
            createSwitchDelegate?.Invoke(this);
        }

        public IAttempt<TValue> Case(TKey key)
        {
            if(dictionary.TryGetValue(key, out var value))
            {
                return Attempt.Success(value);
            }

            return Attempt.Failed<TValue>(new KeyNotFoundException());
        }

        TValue IDictionary<TKey, TValue>.this[TKey key] { 
            get => dictionary.TryGetValue(key, out var value) ? value : default; 
            set => dictionary.TryAdd(key, value); }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => dictionary.Keys;

        ICollection<TValue> IDictionary<TKey, TValue>.Values => dictionary.Values;

        int ICollection<KeyValuePair<TKey, TValue>>.Count => dictionary.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        ISwitch<TKey, TValue> ISwitch<TKey, TValue>.Add(KeyValuePair<TKey, TValue> item)
        {
            dictionary.TryAdd(item.Key, item.Value);
            return this;
        }

        ISwitch<TKey, TValue> ISwitch<TKey, TValue>.Add(TKey key, TValue value)
        {
            dictionary.TryAdd(key, value);
            return this;
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            dictionary.TryAdd(key, value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            dictionary.TryAdd(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            dictionary.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.ContainsKey(item.Key);
        }

        bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (var item in dictionary)
            {
                array = array.Append(item).ToArray();
            }

        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            return dictionary.Remove(key, out var i);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.Remove(item.Key, out var i);
        }

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        private readonly ConcurrentDictionary<TKey, TValue> dictionary;
    }
}
