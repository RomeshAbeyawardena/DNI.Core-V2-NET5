using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
