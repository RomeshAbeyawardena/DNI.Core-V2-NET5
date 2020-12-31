using DNI.Core.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class IncludeableQueryBase<T> : IIncludeableQuery<T>
        where T: class
    {
        IIncludeableQuery<T> IIncludeableQuery<T>.Includes<TSource>(Expression<Func<T, TSource>> includeExpression)
        {
            query = query.Include(includeExpression);
            return this;
        }

        Type IQueryable.ElementType => query.ElementType;

        Expression IQueryable.Expression => query.Expression;

        IQueryProvider IQueryable.Provider => query.Provider;

        protected IncludeableQueryBase(IQueryable<T> query)
        {
            this.query = query;
        }

        private TResult EnsureParametersAreNotNullAndReturnResult<TResult>(
            Func<IQueryable<T>, Expression<Func<T, bool>>, CancellationToken, TResult> queryableDelegate,
            Func<IQueryable<T>, CancellationToken, TResult> queryableDefaultDelegate,
            ref Expression<Func<T, bool>> whereExpression,
            ref CancellationToken? cancellationToken)
        {
            EnsureCancellationTokenIsNotNull(ref cancellationToken);

            if (whereExpression == default)
            {
                return queryableDefaultDelegate(query, cancellationToken.Value);
            }

            return queryableDelegate(query, whereExpression, cancellationToken.Value);
        }

        
        private static void EnsureCancellationTokenIsNotNull(ref CancellationToken? cancellationToken)
        {
            if (cancellationToken == null)
            {
                cancellationToken = CancellationToken.None;
            }

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

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return EnsureParametersAreNotNullAndReturnResult((query, whereExpression, ct) => query.FirstOrDefaultAsync(whereExpression, ct),
                (query, ct) => query.FirstOrDefaultAsync(ct), ref whereExpression, ref cancellationToken);
        }

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return EnsureParametersAreNotNullAndReturnResult((query, whereExpression, ct) => query.SingleOrDefaultAsync(whereExpression, ct),
                (query, ct) => query.SingleOrDefaultAsync(ct), ref whereExpression, ref cancellationToken);
        }

        public async Task<IEnumerable<T>> ToArrayAsync(Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return await EnsureParametersAreNotNullAndReturnResult((query, whereExpression, ct) => query.Where(whereExpression).ToArrayAsync(ct),
                (query, ct) => query.ToArrayAsync(ct), ref whereExpression, ref cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return EnsureParametersAreNotNullAndReturnResult((query, whereExpression, ct) => query.AnyAsync(whereExpression, ct),
                (query, ct) => query.AnyAsync(ct), ref whereExpression, ref cancellationToken);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return EnsureParametersAreNotNullAndReturnResult((query, whereExpression, ct) => query.CountAsync(whereExpression, ct),
                (query, ct) => query.CountAsync(ct), ref whereExpression, ref cancellationToken);
        }

        Task<T> IIncludeableQuery<T>.FirstOrDefaultAsync(CancellationToken? cancellationToken)
        {
            return FirstOrDefaultAsync(null, cancellationToken);
        }

        Task<T> IIncludeableQuery<T>.SingleOrDefaultAsync(CancellationToken? cancellationToken)
        {
            return SingleOrDefaultAsync(null, cancellationToken);
        }

        Task<IEnumerable<T>> IIncludeableQuery<T>.ToArrayAsync(CancellationToken? cancellationToken)
        {
            return ToArrayAsync(null, cancellationToken);
        }

        Task<bool> IIncludeableQuery<T>.AnyAsync(CancellationToken? cancellationToken)
        {
            return AnyAsync(null, cancellationToken);
        }

        Task<int> IIncludeableQuery<T>.CountAsync(CancellationToken? cancellationToken)
        {
            return CountAsync(null, cancellationToken);
        }

        private IQueryable<T> query;
    }
}
