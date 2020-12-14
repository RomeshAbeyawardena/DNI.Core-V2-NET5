using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class CollectionExtensions
    {
        public static void TryAddOrCreate<T>(ref ICollection<T> collection, T item)
        {
            if(collection == null)
            {
                collection = new List<T>();
            }

            collection.Add(item);
        }
    }
}
