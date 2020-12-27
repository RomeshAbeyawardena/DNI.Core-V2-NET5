using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    public static class Builders
    {
        public static IDictionaryBuilder<TKey, TValue> Dictionary<TKey, TValue>()
        {
            return new DictionaryBuilder<TKey, TValue>();
        }

        public static IListBuilder<T> List<T>()
        {
            return new ListBuilder<T>();
        }

        public static IDictionaryBuilder<TKey, TValue> Dictionary<TKey, TValue>(Action<IDictionaryBuilder<TKey, TValue>> buildAction)
        {
            return new DictionaryBuilder<TKey, TValue>(buildAction);
        }

        public static IListBuilder<T> List<T>(Action<IListBuilder<T>> buildAction)
        {
            return new ListBuilder<T>(buildAction);
        }
    }
}
