using System.Collections.Generic;
using System.Linq;

namespace DNI.Core.Abstractions.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> items, IEnumerable<T> newItems)
        {
            var itemsList = items.ToList();

            itemsList.AddRange(newItems);

            return itemsList;
        }
    }
}
