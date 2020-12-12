using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Contracts.Factories
{
    /// <summary>
    /// Represents a factory used for returning a specific <see cref="ICacheService"/> from a dependency container
    /// </summary>
    public interface ICacheServiceFactory
    {
        /// <summary>
        /// Retrieves an <see cref="ICacheService"/> from a dependecy container
        /// </summary>
        /// <param name="cacheServiceType">The type of <see cref="ICacheService"/> to be returned</param>
        /// <returns>An instance of <see cref="ICacheService"/></returns>
        ICacheService GetCacheService(CacheServiceType cacheServiceType);
    }
}
