using System.Collections.Generic;

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
