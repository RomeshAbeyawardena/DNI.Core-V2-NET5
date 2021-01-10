using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IIncludeableQuery<T> : IQueryable<T>, IAsyncEnumerable<T>
        where T : class
    {
        IIncludeableQuery<T> Includes<TSource>(Expression<Func<T, TSource>> includeExpression);

        Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<IEnumerable<T>> ToArrayAsync(
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<bool> AnyAsync(
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<int> CountAsync(
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        
        Task<T> FirstOrDefaultAsync(
            CancellationToken? cancellationToken = null);

        Task<T> SingleOrDefaultAsync(
            CancellationToken? cancellationToken = null);

        Task<IEnumerable<T>> ToArrayAsync(
            CancellationToken? cancellationToken = null);

        Task<bool> AnyAsync(
            CancellationToken? cancellationToken = null);

        Task<int> CountAsync(
            CancellationToken? cancellationToken = null);

    }
}
