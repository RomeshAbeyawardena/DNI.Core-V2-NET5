using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Abstractions
{
    public abstract class CacheServiceBase : ICacheService
    {
        public abstract IAttempt<T> TryGet<T>(string cacheKeyName, SerializerType serializer);
        public abstract IAttempt TrySet<T>(string cacheKeyName, T value, SerializerType serializer);
        
        public abstract Task<IAttempt<T>> TryGetAsync<T>(string cacheKeyName, SerializerType serializer, CancellationToken cancellationToken);
        public abstract Task<IAttempt> TrySetAsync<T>(string cacheKeyName, T value, SerializerType serializer, CancellationToken cancellationToken);

        public CacheServiceType Type { get; }

        protected T Deserialize<T>(IEnumerable<byte> data, SerializerType serializerType)
        {
            var serializer = GetSerializer(serializerType);

            return serializer.Deserialize<T>(data);
        }

        protected IEnumerable<byte> Serialize<T>(T value, SerializerType serializerType)
        {
            var serializer = GetSerializer(serializerType);

            return serializer.Serialize(value);
        }

        protected ISerializer GetSerializer(SerializerType serializerType)
        {
            return serializerFactory.GetSerializer(serializerType);
        }

        protected CacheServiceBase(CacheServiceType cacheServiceType, ISerializerFactory serializerFactory)
        {
            Type = cacheServiceType;
            this.serializerFactory = serializerFactory;
        }

        private readonly ISerializerFactory serializerFactory;
    }
}
