using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;

namespace DNI.Core.Shared
{
    public static class Definition
    {
        public static IDefinition<T> Create<T>()
        {
            return new Definition<T>();
        }

        public static IDefinition<Assembly> CreateAssemblyDefinition()
        {
            return Create<Assembly>();
        }

        public static IDefinition<Type> CreateTypeDefinition()
        {
            return Create<Type>();
        }

        public static IDefinition<T> Create<T>(Action<IDefinition<T>> initializerDelegate)
        {
            return new Definition<T>(initializerDelegate);
        }

        public static IDefinition<Assembly> CreateAssemblyDefinition(Action<IDefinition<Assembly>> initializerDelegate)
        {
            return Create<Assembly>(initializerDelegate);
        }

        public static IDefinition<Type> CreateTypeDefinition(Action<IDefinition<Type>> initializerDelegate)
        {
            return Create<Type>(initializerDelegate);
        }
    }

    class Definition<T> : IDefinition<T>
    {
        public IEnumerable<T> Items => itemBag.ToArray();

        public IDefinition<T> Add(T item)
        {
            itemBag.Add(item);
            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return itemBag.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return itemBag.GetEnumerator();
        }

        public IDefinition<T> AddRange(IEnumerable<T> items)
        {
            items.ForEach(item => Add(item));
            return this;
        }

        internal Definition()
        {
            itemBag  = new ConcurrentBag<T>();
        }

        internal Definition(Action<IDefinition<T>> initializerDelegate)
            : this()
        {
            initializerDelegate(this);
        }

        internal Definition(IEnumerable<T> items)
        {
            itemBag = new ConcurrentBag<T>(items);
        }

        private ConcurrentBag<T> itemBag;
    }
}
