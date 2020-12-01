using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface ICacheService
    {
        CacheServiceType Type { get; }
        IAttempt<T> TryGet<T>(string cacheKeyName, SerializerType serializer);
        IAttempt TrySet<T>(string cacheKeyName, T value, SerializerType serializer);
        Task<IAttempt<T>> TryGetAsync<T>(string cacheKeyName, SerializerType serializer, CancellationToken cancellationToken);
        Task<IAttempt> TrySetAsync<T>(string cacheKeyName, T value, SerializerType serializer, CancellationToken cancellationToken);
    }
}
