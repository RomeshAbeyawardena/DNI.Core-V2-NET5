using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Providers;
using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class CacheProviderBase : ICacheProvider
    {
        public CacheServiceType CacheServiceType { get; set; }
        public SerializerType SerializerType { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected CacheProviderBase(ICacheServiceFactory cacheServiceFactory,
            ICacheDependency cacheDependency)
        {
            concurrentDictionary = new ConcurrentDictionary<Type, SemaphoreSlim>();
            this.cacheServiceFactory = cacheServiceFactory;
            this.cacheDependency = cacheDependency;
        }

        protected void Dispose(bool gc)
        {
            if (gc)
            {
                foreach (var keyValuePair in concurrentDictionary)
                {
                    keyValuePair.Value.Dispose();
                }
            }
        }

        protected async Task<IEnumerable<T>> Get<T>(string cacheKey, 
            Func<CancellationToken, Task<IEnumerable<T>>> action, 
            CancellationToken cancellationToken)
        {
            var cacheService = cacheServiceFactory.GetCacheService(CacheServiceType);
            
            var semaphoreSlim = concurrentDictionary.GetOrAdd(typeof(T), new SemaphoreSlim(1));

            await semaphoreSlim.WaitAsync();
   
            var attempt = await cacheService
                .TryGetAsync<IEnumerable<T>>(cacheKey, SerializerType, cancellationToken);

            if (attempt.Successful 
                && await cacheDependency.IsValid(cacheKey, cancellationToken))
            {
                return attempt.Result;
            }

            var result = await action(cancellationToken);

            if(result != null)
            {
                var setAttempt = await cacheService
                    .TrySetAsync(cacheKey, result, SerializerType, cancellationToken);

                if (!setAttempt.Successful)
                {
                    throw new InvalidOperationException();
                }

                await cacheDependency.Set(cacheKey, cancellationToken);
            }

            semaphoreSlim.Release();

            return result;
        }

        private readonly ConcurrentDictionary<Type, SemaphoreSlim> concurrentDictionary;
        private readonly ICacheServiceFactory cacheServiceFactory;
        private readonly ICacheDependency cacheDependency;
    }
}
