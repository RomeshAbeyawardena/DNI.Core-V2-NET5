using System;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Abstractions.Factories
{
    internal class CacheServiceFactory : ImplementationFactoryBase<ICacheService, CacheServiceType>, ICacheServiceFactory
    {
        public ICacheService GetCacheService(CacheServiceType cacheServiceType)
        {
            return GetServiceType(cacheServiceType);
        }

        public CacheServiceFactory(IServiceProvider serviceProvider)
           : base(serviceProvider, service => service.Type)
        {

        }
    }
}
