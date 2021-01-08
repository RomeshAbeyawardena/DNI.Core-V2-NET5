using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IDataService<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> ToArray(CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> ToArray(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<int> SaveChanges(CancellationToken cancellationToken);
        Task<int> Save(TEntity entity, CancellationToken cancellationToken);
    }
}
