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
    public abstract class ServiceRegistrationBase<TDbContext> 
        : Core.Abstractions.Bootstrap.ServiceRegistrationBase
        where TDbContext : DbContext
    {
        public DbContextMethod DbContextMethod { get; }
        
        public override IEnumerable<Assembly> ServiceAssemblies { get; }
        public override IEnumerable<Assembly> DomainAssemblies { get; }
        public override IEnumerable<string> ScannedTypes { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
            Configure(services);

            services
                .RegisterDbContextWithRepositories<TDbContext>(DbContextMethod, 
                    dbContextOptionsServiceInjectionAction: Builder, 
                    serviceLifetime: ServiceLifetime);
        }

        public abstract void Configure(IServiceCollection services);

        public abstract void Builder(IServiceProvider serviceProvider, DbContextOptionsBuilder builder);

        protected ServiceRegistrationBase(DbContextMethod dbContextMethod = DbContextMethod.DbContextPool, 
            ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
            : base(serviceLifetime)
        {
            DbContextMethod = dbContextMethod;
        }

    }

    public abstract class ServiceRegistration<TApplicationSettings, TDbContext> : ServiceRegistrationBase<TDbContext>
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
