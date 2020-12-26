﻿using DNI.Core.Abstractions.Extensions;
using DNI.Core.Abstractions.Services;
using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Extensions;
using DNI.Core.Shared.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.Core.Abstractions
{

    internal class DefaultServiceRegistration : ServiceRegistrationBase
    {
        public override IServiceCollection RegisterServices(IServiceCollection services)
        {
            services
                .AddSingleton(SystemClock.CreateDefault())
                .AddOptions<EncryptionOptions>();
            return services
                .AddSingleton(Newtonsoft.Json.JsonSerializer.CreateDefault())
                .AddSingleton(typeof(IModelEncryptionService<>), typeof(DefaultModelEncryptionService<>))
                .AddSingleton(typeof(IChangeTracker<>), typeof(DefaultChangeTracker<>))
                .ScanForTypes(typeDefinition => typeDefinition.AddRange(ScanTypes), 
                    AssemblyDefinitions.ToArray());
        }

        private static IDefinition<Assembly> AssemblyDefinitions => 
            GetAssemblies(assembly => assembly.GetAssembly<DefaultServiceRegistration>());

        private static IEnumerable<string> ScanTypes => new []{ "Factory", "Serializer", "Service" };
    }
}
