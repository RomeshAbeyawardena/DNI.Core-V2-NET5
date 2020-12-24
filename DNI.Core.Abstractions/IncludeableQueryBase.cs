using DNI.Core.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DNI.Core.Abstractions
{
    public abstract class IncludeableQueryBase<T> : IIncludeableQuery<T>
        where T: class
    {
        Type IQueryable.ElementType => query.ElementType;

        Expression IQueryable.Expression => query.Expression;

        IQueryProvider IQueryable.Provider => query.Provider;

        protected IncludeableQueryBase(IQueryable<T> query)
        {
            this.query = query;
        }

        public IIncludeableQuery<T> Includes<TSource>(Expression<Func<T, TSource>> includeExpression)
        {
            query = query.Include(includeExpression);
            return this;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return query.GetEnumerator();
        }

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            if(query is IAsyncEnumerable<T> asyncEnumerable)
            {
                return asyncEnumerable.GetAsyncEnumerator(cancellationToken);
            }

             throw new NotSupportedException();
        }

        private IQueryable<T> query;
    }
}
