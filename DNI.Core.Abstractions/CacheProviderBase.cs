using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Providers;
using DNI.Core.Shared.Enumerations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        protected CacheProviderBase(
            ILogger logger,
            ICacheServiceFactory cacheServiceFactory,
            ICacheDependency cacheDependency)
        {
            concurrentDictionary = new ConcurrentDictionary<Type, SemaphoreSlim>();
            this.logger = logger;
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
            else
            {
                if(attempt.Exception != null)
                {
                    logger.LogError(attempt.Exception, "Unable to get cache item for {0} from {1} using {2}", 
                        cacheKey, CacheServiceType, SerializerType);
                }
                else
                    logger.LogInformation("CacheDependency is currently invalidated");
            }

            var result = await action(cancellationToken);

            if(result != null)
            {
                var setAttempt = await cacheService
                    .TrySetAsync(cacheKey, result, SerializerType, cancellationToken);

                if (!setAttempt.Successful)
                {
                    logger.LogError(setAttempt.Exception, "Unable to set cache key {0} from {1} using {2}", 
                        cacheKey, CacheServiceType, SerializerType);
                    throw new InvalidOperationException();
                }

                await cacheDependency.Set(cacheKey, cancellationToken);
            }

            semaphoreSlim.Release();

            return result;
        }

        private readonly ILogger logger;
        private readonly ConcurrentDictionary<Type, SemaphoreSlim> concurrentDictionary;
        private readonly ICacheServiceFactory cacheServiceFactory;
        private readonly ICacheDependency cacheDependency;
    }
}
