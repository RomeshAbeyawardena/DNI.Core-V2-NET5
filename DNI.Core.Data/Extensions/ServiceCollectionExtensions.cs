using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;

namespace DNI.Core.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDbContextWithRepositories<TDbContext>(
            this IServiceCollection services, 
            DbContextMethod dbContextMethod = DbContextMethod.SingleInstance,
            int poolSize = 128,
            Action<DbContextOptionsBuilder> dbContextOptionsAction = default,
            Action<IServiceProvider, DbContextOptionsBuilder> dbContextOptionsServiceInjectionAction = default,
            ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
            where TDbContext : DbContext
        {
            return services
                .RegisterDbContext<TDbContext>( 
                    dbContextMethod, 
                    poolSize, 
                    dbContextOptionsAction, 
                    dbContextOptionsServiceInjectionAction, 
                    serviceLifetime)
                .RegisterDbContextEntityRepostories<TDbContext>(serviceLifetime);
        }

        private static IServiceCollection RegisterDbContextEntityRepostories<TDbContext>(
            this IServiceCollection services, 
            ServiceLifetime serviceLifetime)
            where TDbContext : DbContext
        {
            var dbContextType = typeof(TDbContext);
            var genericRepository = typeof(IRepository<>);
            var genericAsyncRepository = typeof(IAsyncRepository<>);
            var genericRepositoryImplementation = typeof(EntityFrameworkRepository<,>);

            var entityTypes = dbContextType.GetProperties()
                .Where(property => property.PropertyType.IsGenericType)
                .Select(property => property.PropertyType.GetGenericArguments().FirstOrDefault());

            foreach(var entityType in entityTypes)
            {
                var genericImplementation = genericRepositoryImplementation.MakeGenericType(dbContextType, entityType);

                if(entityType == null)
                {
                    Debug.Fail("Entity type is null", "Entity type within DbContext not found");
                    continue;
                }

                services.Add(ServiceDescriptor.Describe(genericRepository.MakeGenericType(entityType), 
                    genericImplementation, serviceLifetime));

                services.Add(ServiceDescriptor.Describe(genericAsyncRepository.MakeGenericType(entityType), 
                    genericImplementation, serviceLifetime));
            }

            return services;
        }

    }
}
