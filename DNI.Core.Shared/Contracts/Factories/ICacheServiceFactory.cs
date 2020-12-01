using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface ICacheServiceFactory
    {
        ICacheService GetCacheService(CacheServiceType cacheServiceType);
    }
}
