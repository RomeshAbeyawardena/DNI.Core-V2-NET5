using DNI.Core.Abstractions.Extensions;
using DNI.Core.Abstractions.Services;
using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Extensions;
using DNI.Core.Shared.Handlers;
using DNI.Core.Shared.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace DNI.Core.Abstractions
{
    internal class DefaultServiceRegistration : ServiceRegistrationBase
    {
        public static IServiceRegistration Create()
        {
            return new DefaultServiceRegistration();
        }

        public override IServiceCollection RegisterServices(IServiceCollection services)
        {
            services
                .AddSingleton(SystemClock.CreateDefault())
                .AddSingleton(Handler.Default)
                .AddSingleton(Builders.Default)
                .AddOptions<EncryptionOptions>();
            return services
                .AddSingleton(RandomNumberGenerator.Create())
                .AddSingleton(serviceProvider => RandomStringGenerator.Create(serviceProvider.GetRequiredService<RandomNumberGenerator>()))
                .AddSingleton(Newtonsoft.Json.JsonSerializer.CreateDefault())
                .AddSingleton(typeof(IModelEncryptionService<>), typeof(DefaultModelEncryptionService<>))
                .AddSingleton(typeof(IChangeTracker<>), typeof(DefaultChangeTracker<>))
                .ScanForTypes(typeDefinition => typeDefinition.AddRange(ScanTypes), 
                    AssemblyDefinitions.ToArray());
        }

        private DefaultServiceRegistration()
        {

        }

        private static IDefinition<Assembly> AssemblyDefinitions => 
            GetAssemblies(assembly => assembly.GetAssembly<DefaultServiceRegistration>());

        private static IEnumerable<string> ScanTypes => new []{ "Factory", "Serializer", "Service" };
    }
}
