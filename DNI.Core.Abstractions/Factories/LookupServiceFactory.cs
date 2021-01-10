using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Factories
{
    internal class LookupServiceFactory : ILookupServiceFactory
    {
        public LookupServiceFactory(IServiceProvider serviceProvider)
        {
            
        }

        public ILookupService GetLookupService<TEntity>()
        {
            var entityType = typeof(TEntity);

            var lookupServices = serviceProvider
                .GetServices(typeof(ILookupService))
                .Select(s => s as ILookupService);

            return lookupServices.FirstOrDefault(ls => ls.EntityType == entityType);
        }

        private readonly IServiceProvider serviceProvider;
    }
}
