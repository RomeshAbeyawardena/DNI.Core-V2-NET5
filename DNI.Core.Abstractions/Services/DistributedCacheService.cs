using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Shared;
using DNI.Core.Shared.Abstractions;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Abstractions.Services
{
    internal class DistributedCacheService : CacheServiceBase
    {
        public override IAttempt<T> TryGet<T>(string cacheKeyName, SerializerType serializerType)
        {
            var result = distributedCache.Get(cacheKeyName);
            if(result == null || result.Length < 1)
            {
                return Attempt.Failed<T>(new NullReferenceException("Item with the key {cacheKeyName} not found"));
            }

            return Attempt.Success(Deserialize<T>(result, serializerType));
        }

        public override IAttempt TrySet<T>(string cacheKeyName, T value, SerializerType serializerType)
        {
            var result = Serialize(value, serializerType);
            distributedCache.Set(cacheKeyName, result.ToArray(), distributedCacheEntryOptions);

            return Attempt.Success();
        }

        public override async Task<IAttempt<T>> TryGetAsync<T>(string cacheKeyName, SerializerType serializerType, CancellationToken cancellationToken)
        {
            var result = await distributedCache.GetAsync(cacheKeyName, cancellationToken);
            if(result == null || result.Length == 0)
            {
                return Attempt.Failed<T>(new NullReferenceException($"Item with the key {cacheKeyName} not found"));
            }

            return Attempt.Success(Deserialize<T>(result, serializerType));
        }

        public override async Task<IAttempt> TrySetAsync<T>(string cacheKeyName, T value, SerializerType serializerType, CancellationToken cancellationToken)
        {
            var result = Serialize(value, serializerType);
            await distributedCache.SetAsync(cacheKeyName, result.ToArray(), distributedCacheEntryOptions, cancellationToken);

            return Attempt.Success();
        }

        public DistributedCacheService(ISerializerFactory serializerFactory, 
            IDistributedCache distributedCache,
            IOptions<DistributedCacheEntryOptions> distributedCacheEntryOptions)
            : base(CacheServiceType.Distributed, serializerFactory)
        {
            this.distributedCache = distributedCache;
            this.distributedCacheEntryOptions = distributedCacheEntryOptions.Value;
        }

        private readonly DistributedCacheEntryOptions distributedCacheEntryOptions;
        private readonly IDistributedCache distributedCache;
    }
}
