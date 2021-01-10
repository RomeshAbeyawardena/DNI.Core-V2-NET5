using AutoMapper;
using DNI.Core.Abstractions;
using DNI.Core.Abstractions.Extensions;
using DNI.Core.Abstractions.Setters;
using DNI.Core.Data.Extensions;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.Core.Data.Bootstrap
{
    public abstract class ServiceRegistration<TDbContext> : ServiceRegistrationBase
        where TDbContext : DbContext
    {
        public DbContextMethod DbContextMethod { get; }
        public ServiceLifetime ServiceLifetime { get; }
        
        public abstract IEnumerable<Assembly> ServiceAssemblies { get; }
        public abstract IEnumerable<Assembly> DomainAssemblies { get; }
        public abstract IEnumerable<string> ScannedTypes { get; }

        public override IServiceCollection RegisterServices(IServiceCollection services)
        {
            ConfigureServices(services);

            return services
                .AddMediatR(ServiceAssemblies.ToArray())
                .AddAutoMapper(DomainAssemblies.ToArray())
                .RegisterDbContextWithRepositories<TDbContext>(DbContextMethod, 
                    dbContextOptionsServiceInjectionAction: Builder, 
                    serviceLifetime: ServiceLifetime)
                .ScanForTypes(scanDefinition => scanDefinition
                    .AddRange(ScannedTypes), 
                ServiceAssemblies.ToArray());
        }

        public abstract void ConfigureServices(IServiceCollection services);

        public abstract void Builder(IServiceProvider serviceProvider, DbContextOptionsBuilder builder);

        protected IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings> GetSettingsEncryptionClassificationSetter<TApplicationSettings>()
        {
            return ApplicationSettingsEncryptionClassificationSetter.Create<TApplicationSettings>();
        }

        protected ServiceRegistration(DbContextMethod dbContextMethod = DbContextMethod.DbContextPool, 
            ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            DbContextMethod = dbContextMethod;
            ServiceLifetime = serviceLifetime;
        }

    }

    public abstract class ServiceRegistration<TApplicationSettings, TDbContext> : ServiceRegistration<TDbContext>
        where TDbContext : DbContext
    {
        protected ServiceRegistration(DbContextMethod dbContextMethod = DbContextMethod.DbContextPool, 
            ServiceLifetime serviceLifetime = ServiceLifetime.Transient) 
            : base(dbContextMethod, serviceLifetime)
        {
        }

        protected IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings> GetSettingsEncryptionClassificationSetter()
        {
            return GetSettingsEncryptionClassificationSetter<TApplicationSettings>();
        }
    }
}
