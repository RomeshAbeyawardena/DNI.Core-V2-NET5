using DNI.Core.Abstractions.Setters;
using DNI.Core.Shared.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using System.Text;
using System.Threading.Tasks;
using DNI.Core.Abstractions.Extensions;

namespace DNI.Core.Abstractions.Bootstrap
{
    public abstract class ServiceRegistrationBase : Abstractions.ServiceRegistrationBase
    {
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
                .ScanForTypes(scanDefinition => scanDefinition
                    .AddRange(ScannedTypes), 
                ServiceAssemblies.ToArray());
        }

        public abstract void ConfigureServices(IServiceCollection services);

        protected IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings> GetSettingsEncryptionClassificationSetter<TApplicationSettings>()
        {
            return ApplicationSettingsEncryptionClassificationSetter.Create<TApplicationSettings>();
        }

        protected ServiceRegistrationBase(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            ServiceLifetime = serviceLifetime;
        }

    }
}
