using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IRepository<T>
    {
        IQueryable<T> Query { get; }
        IQueryable<T> EnableTracking(IQueryable<T> query, bool enableTracking = true, bool enableIdentityResolution = true);
        T Find(params object[] keys);
        int SaveChanges();
        void Add(T result);
        void Update(T result);
        void Remove(T result);
    }

    public interface IAsyncRepository<T> : IRepository<T>
    {
        Task<T> FindAsync(
            CancellationToken? cancellationToken = default, 
            params object[] keys);
        Task<T> FirstOrDefaultAsync(IQueryable<T> query = default,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<T> SingleOrDefaultAsync(IQueryable<T> query = default,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<IEnumerable<T>> ToArrayAsync(IQueryable<T> query = default,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<bool> AnyAsync(IQueryable<T> query,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<int> CountAsync(IQueryable<T> query = default,
            Expression<Func<T, bool>> whereExpression = default,
            CancellationToken? cancellationToken = null);

        Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null);
    }
}
