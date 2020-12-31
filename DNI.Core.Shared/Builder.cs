using DNI.Core.Shared.Contracts;
using System;

namespace DNI.Core.Shared
{
    /// <inheritdoc cref="IBuilder" />
    internal class Builder : IBuilder
    {
        public IListBuilder<T> Create<T>()
        {
            return new ListBuilder<T>();
        }

        public IDictionaryBuilder<TKey, TValue> Create<TKey, TValue>()
        {
            return new DictionaryBuilder<TKey, TValue>();
        }

        public IListBuilder<T> Create<T>(Action<IListBuilder<T>> buildAction)
        {
            return new ListBuilder<T>(buildAction);
        }

        public IDictionaryBuilder<TKey, TValue> Create<TKey, TValue>(Action<IDictionaryBuilder<TKey, TValue>> buildAction)
        {
            return new DictionaryBuilder<TKey, TValue>(buildAction);
        }

        public ISwitch<TKey, TValue> Create<TKey, TValue>(Action<ISwitch<TKey, TValue>> buildAction)
        {
            return Switch.Create<TKey, TValue>();
        }
    }
}
