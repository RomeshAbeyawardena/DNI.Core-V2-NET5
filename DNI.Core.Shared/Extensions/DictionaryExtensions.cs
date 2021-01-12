﻿using System.Collections.Generic;

namespace DNI.Core.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool TryAddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return true;
            }
            
            return dictionary.TryAdd(key, value);
            
        }
    }
}
