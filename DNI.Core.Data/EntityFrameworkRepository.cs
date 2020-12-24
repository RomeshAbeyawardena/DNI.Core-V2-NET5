using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Shared.Contracts;

namespace DNI.Core.Data
{
    class EntityFrameworkRepository<TDbContext, T> : IRepository<T>, IAsyncRepository<T>
        where TDbContext : DbContext
        where T : class
    {
        public EntityFrameworkRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public IQueryable<T> Query => DbSet;

        public void Add(T result)
        {
            DbContext.Add(result);
        }

        public Task<bool> AnyAsync(IQueryable<T> query, Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return EnsureParametersAreNotNullAndReturnResult(
                (query, whereExpression, ct) => query.AnyAsync(whereExpression, ct),
                (query, ct) => query.AnyAsync(ct),
                ref query, ref whereExpression, ref cancellationToken);
        }

        public Task<int> CountAsync(IQueryable<T> query, Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return EnsureParametersAreNotNullAndReturnResult(
                (query, whereExpression, ct) => query.CountAsync(whereExpression, ct),
                (query, ct) => query.CountAsync(ct),
                ref query, ref whereExpression, ref cancellationToken);
        }

        public IQueryable<T> EnableTracking(IQueryable<T> query, bool enableTracking, bool enableIdentityResolution)
        {
            if (enableTracking)
            {
                return query.AsTracking();
            }

            return enableIdentityResolution
                ? query.AsNoTrackingWithIdentityResolution()
                : query.AsNoTracking();
        }
        public Task<T> FirstOrDefaultAsync(IQueryable<T> query, Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return EnsureParametersAreNotNullAndReturnResult(
                (query, whereExpression, ct) => query.FirstOrDefaultAsync(whereExpression, ct),
                (query, ct) => query.FirstOrDefaultAsync(ct),
                ref query, ref whereExpression, ref cancellationToken);
        }

        public void Remove(T result)
        {
            DbContext.Remove(result);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken? cancellationToken)
        {
            EnsureCancellationTokenIsNotNull(ref cancellationToken);
            return DbContext.SaveChangesAsync(cancellationToken.Value);
        }

        public Task<T> SingleOrDefaultAsync(IQueryable<T> query, Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return EnsureParametersAreNotNullAndReturnResult(
                (query, whereExpression, ct) => query.SingleOrDefaultAsync(whereExpression, ct),
                (query, ct) => query.SingleOrDefaultAsync(ct),
                ref query, ref whereExpression, ref cancellationToken);
        }

        public async Task<IEnumerable<T>> ToArrayAsync(IQueryable<T> query, Expression<Func<T, bool>> whereExpression, CancellationToken? cancellationToken)
        {
            return await EnsureParametersAreNotNullAndReturnResult(
                (query, whereExpression, ct) => query.ToArrayAsync(ct),
                (query, ct) => query.ToArrayAsync(ct),
                ref query, ref whereExpression, ref cancellationToken);
        }

        public void Update(T result)
        {
            DbContext.Update(result);
        }

        protected TDbContext DbContext { get; }
        protected DbSet<T> DbSet { get; }

        private static void EnsureCancellationTokenIsNotNull(ref CancellationToken? cancellationToken)
        {
            if (cancellationToken == null)
            {
                cancellationToken = CancellationToken.None;
            }

        }

        private TResult EnsureParametersAreNotNullAndReturnResult<TResult>(
            Func<IQueryable<T>, Expression<Func<T, bool>>, CancellationToken, TResult> queryableDelegate,
            Func<IQueryable<T>, CancellationToken, TResult> queryableDefaultDelegate,
            ref IQueryable<T> query,
            ref Expression<Func<T, bool>> whereExpression,
            ref CancellationToken? cancellationToken)
        {
            if (query == default)
            {
                query = Query;
            }

            EnsureCancellationTokenIsNotNull(ref cancellationToken);

            if (whereExpression == default)
            {
                return queryableDefaultDelegate(query, cancellationToken.Value);
            }

            return queryableDelegate(query, whereExpression, cancellationToken.Value);
        }

        public T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public Task<T> FindAsync(CancellationToken? cancellationToken, params object[] keys)
        {
            EnsureCancellationTokenIsNotNull(ref cancellationToken);
            return DbSet.FindAsync(keys, cancellationToken.Value).AsTask();
        }

        public IIncludeableQuery<T> Include<TSelector>(IQueryable<T> query, Expression<Func<T, TSelector>> includeExpression)
        {
            return new IncludeableQuery<T>(query).Includes(includeExpression);
        }
    }
}
