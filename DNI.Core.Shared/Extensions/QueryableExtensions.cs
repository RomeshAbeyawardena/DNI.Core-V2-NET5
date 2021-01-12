using DNI.Core.Shared.Contracts;
using System.Linq;

namespace DNI.Core.Shared.Extensions
{
    public static class QueryableExtensions
    {
        public static IPager<T> AsPager<T>(this IQueryable<T> query)
        {
            return new Pager<T>(query);
        }
    }
}
