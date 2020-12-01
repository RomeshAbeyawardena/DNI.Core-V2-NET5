using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Shared.Contracts;

namespace DNI.Core.Abstractions
{
    public abstract class ServiceBase
    {
        protected ServiceBase()
        {

        }

        protected async Task PrepareForInsertOrUpdateAsync<T>(
            IAsyncRepository<T> repository,
            IQueryable<T> query,
            Func<T, IAsyncRepository<T>, IQueryable<T>, CancellationToken, Task<T>> updateConditionExpression,
            Action<T, T> preUpdateDelegate,
            T newEntity,
            CancellationToken cancellationToken)
        {
            var foundEntity = await updateConditionExpression(newEntity, repository, query, cancellationToken);
            if(foundEntity == null)
            {
                repository.Add(newEntity);
                return;
            }

            preUpdateDelegate?.Invoke(foundEntity, newEntity);
            repository.Update(newEntity);
        }
    }
}
