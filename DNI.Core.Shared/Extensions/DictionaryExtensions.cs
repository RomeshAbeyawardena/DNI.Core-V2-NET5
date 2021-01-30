using DNI.Core.Shared.Contracts.Builders;
using System.Collections.Generic;
using System.Linq;

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

        public static IDictionaryBuilder<TKey, TValue> ToBuilder<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new DictionaryBuilder<TKey, TValue>(build => { 
                if(dictionary.Keys.Any())
                    build.AddRange(dictionary);
            });
        }
    }
}
