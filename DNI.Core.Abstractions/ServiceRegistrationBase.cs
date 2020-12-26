using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DNI.Core.Abstractions
{
    public abstract class ServiceRegistrationBase : IServiceRegistration
    {
        public abstract IServiceCollection RegisterServices(IServiceCollection services);

        protected static IDefinition<Assembly> GetAssemblies(Action<IDefinition<Assembly>> defineAssemblies)
        {
            var assemblyDefinition = Definition.CreateAssemblyDefinition(defineAssemblies);

            return assemblyDefinition;
        }
    }
}
