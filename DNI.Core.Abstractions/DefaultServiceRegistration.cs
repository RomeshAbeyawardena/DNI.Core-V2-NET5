using DNI.Core.Abstractions.Extensions;
using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.Core.Abstractions
{

    internal class DefaultServiceRegistration : ServiceRegistrationBase
    {
        public override IServiceCollection RegisterServices(IServiceCollection services)
        {
            return services
                .AddSingleton(SystemClock.CreateDefault())
                .AddSingleton(Newtonsoft.Json.JsonSerializer.CreateDefault())
                .ScanForTypes(typeDefinition => typeDefinition.AddRange(ScanTypes), 
                    AssemblyDefinitions.ToArray());
        }

        private static IDefinition<Assembly> AssemblyDefinitions => 
            GetAssemblies(assembly => assembly.GetAssembly<DefaultServiceRegistration>());

        private static IEnumerable<string> ScanTypes => new []{ "Factory", "Serializer", "Service" };
    }
}
