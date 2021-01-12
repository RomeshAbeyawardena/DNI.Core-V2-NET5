using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IDataService<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> ToArrayAsync(CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> ToArrayAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
