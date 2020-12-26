using AutoMapper;
using DNI.Core.Abstractions;
using DNI.Core.Data.Extensions;
using DNI.Core.Shared.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Data
{
    public abstract class ServiceRegistration<TDbContext> : ServiceRegistrationBase
        where TDbContext : DbContext
    {
        public DbContextMethod DbContextMethod { get; }
        public ServiceLifetime ServiceLifetime { get; }
        
        public abstract IEnumerable<Assembly> ServiceAssemblies { get; }
        public abstract IEnumerable<Assembly> DomainAssemblies { get; }

        protected ServiceRegistration(DbContextMethod dbContextMethod = DbContextMethod.DbContextPool, 
            ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            DbContextMethod = dbContextMethod;
            ServiceLifetime = serviceLifetime;
        }

        public override IServiceCollection RegisterServices(IServiceCollection services)
        {
            ConfigureServices(services);

            return services
                .AddMediatR(ServiceAssemblies.ToArray())
                .AddAutoMapper(DomainAssemblies.ToArray())
                .RegisterDbContextWithRepositories<TDbContext>(DbContextMethod, 
                    dbContextOptionsServiceInjectionAction: Builder, 
                    serviceLifetime: ServiceLifetime);
        }

        public abstract void ConfigureServices(IServiceCollection services);

        public abstract void Builder(IServiceProvider serviceProvider, DbContextOptionsBuilder builder);
    }
}
