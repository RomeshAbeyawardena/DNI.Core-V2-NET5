using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    internal class ListBuilder<T> : IListBuilder<T>
    {
        public IListBuilder<T> Add(T item)
        {
            list.Add(item);
            return this;
        }

        public IListBuilder<T> AddRange(IEnumerable<T> items)
        {
            items.ForEach(item => Add(item));
            return this;
        }

        T IReadOnlyList<T>.this[int index] => list.ElementAt(index);

        int IReadOnlyCollection<T>.Count => list.Count;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        internal ListBuilder()
        {
            list = new ConcurrentBag<T>();
        }

        internal ListBuilder(Action<IListBuilder<T>> buildAction)
            : this()
        {
            buildAction(this);
        }

        private readonly ConcurrentBag<T> list;

    }
}
