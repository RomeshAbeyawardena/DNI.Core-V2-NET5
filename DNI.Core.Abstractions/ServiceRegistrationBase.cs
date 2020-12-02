using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public static class ServiceRegistration 
    {
        public static IServiceCollection RegisterServices<TServiceRegistration>(IServiceCollection services)
            where TServiceRegistration : IServiceRegistration
        {
            var serviceRegistration = Activator.CreateInstance<TServiceRegistration>();
            serviceRegistration.RegisterServices(services);
            return services;
        }
    }

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
