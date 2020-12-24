using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IIncludeableQuery<T> : IQueryable<T>, IAsyncEnumerable<T>
        where T : class
    {
        IIncludeableQuery<T> Includes<TSource>(Expression<Func<T, TSource>> includeExpression);
    }
}
