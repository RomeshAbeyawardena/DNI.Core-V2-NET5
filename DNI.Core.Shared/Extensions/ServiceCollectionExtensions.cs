using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Contracts;
using System.Reflection;

namespace DNI.Core.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDbContext<TDbContext>(
            this IServiceCollection services,
            DbContextMethod dbContextMethod = DbContextMethod.SingleInstance,
            int poolSize = 128,
            Action<DbContextOptionsBuilder> dbContextOptionsAction = default,
            Action<IServiceProvider, DbContextOptionsBuilder> dbContextOptionsServiceInjectionAction = default,
            ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
            where TDbContext : DbContext
        {
            switch (dbContextMethod)
            {
                case DbContextMethod.DbContextFactory:
                    if(dbContextOptionsAction != default)
                    { 
                        services.AddDbContextFactory<TDbContext>(dbContextOptionsAction, serviceLifetime);
                    }
                    else if(dbContextOptionsServiceInjectionAction != default)
                    {
                        services.AddDbContextFactory<TDbContext>(dbContextOptionsServiceInjectionAction, serviceLifetime);
                    }
                    else
                    {
                        services.AddDbContextFactory<TDbContext>(lifetime: serviceLifetime);
                    }
                    return services;
                case DbContextMethod.DbContextPool:
                    if(dbContextOptionsAction != default)
                    { 
                        services.AddDbContextPool<TDbContext>(dbContextOptionsAction, poolSize);
                    }
                    else if(dbContextOptionsServiceInjectionAction != default)
                    {
                        services.AddDbContextPool<TDbContext>(dbContextOptionsServiceInjectionAction, poolSize);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Either a {nameof(dbContextOptionsAction)} or {nameof(dbContextOptionsServiceInjectionAction)} must be supplied for {dbContextMethod}");
                    }
                    return services;
                case DbContextMethod.SingleInstance:
                    if(dbContextOptionsAction != default)
                    { 
                        services.AddDbContext<TDbContext>(dbContextOptionsAction, serviceLifetime);
                    }
                    else if(dbContextOptionsServiceInjectionAction != default)
                    {
                        services.AddDbContext<TDbContext>(dbContextOptionsServiceInjectionAction, serviceLifetime);
                    }
                    else
                    {
                        services.AddDbContext<TDbContext>(serviceLifetime, serviceLifetime);
                    }
                    return services;
                default:
                    throw new InvalidOperationException("Invalid DbContextMethod specified");
            }
        } 
    }
}
