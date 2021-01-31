using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class ListExtensions
    {
        public static void Deallocate<T>(ref List<T> list,
            GCCollectionMode collectionMode = GCCollectionMode.Forced,
            bool blocking = false)
        {
            list.Clear();
            list.TrimExcess();

            var generation = GC.GetGeneration(list);

            list = null;
            GC.Collect(
                    generation, 
                    GCCollectionMode.Forced, 
                    blocking);
        }
    }
}
