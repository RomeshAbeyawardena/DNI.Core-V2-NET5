using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Core.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> itemAction)
        {
            foreach (var item in items)
            {
                itemAction(item);
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Func<T, T> itemAction)
        {
            var itemList = new List<T>();
            foreach (var item in items)
            {
                itemList.Add(
                    itemAction(item));
            }

            return itemList.ToArray();
        }

        public static IListBuilder<T> ToBuilder<T>(this IEnumerable<T> items)
        {
            return new ListBuilder<T>(build => { 
                if(items.Any()) 
                    build.AddRange(items); 
                });
        }
    }
}
