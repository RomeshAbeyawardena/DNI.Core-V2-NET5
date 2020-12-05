using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices<TServiceRegistration>(this IServiceCollection services, bool registerInternalServices = true)
            where TServiceRegistration : IServiceRegistration
        {
            var serviceRegistration = Activator.CreateInstance<TServiceRegistration>();
            if (registerInternalServices)
            {
                new DefaultServiceRegistration().RegisterServices(services);
            }
            serviceRegistration.RegisterServices(services);
            return services;
        }

        public static IServiceCollection ScanForTypes(this IServiceCollection services, 
            Action<IDefinition<string>> scanTypeDefinitionAction, 
            params Assembly[] assemblies)
        {
            return services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classFilter => classFilter.Where(DefineScannerFilters(scanTypeDefinitionAction)))
            .AsImplementedInterfaces());
        }

        public static Func<Type, bool> DefineScannerFilters(Action<IDefinition<string>> definitionTypesAction)
        {
            var scannerDefinitionTypes = Definition.Create<string>();

            definitionTypesAction(scannerDefinitionTypes);
            var scannerDefinitionTypeArray = scannerDefinitionTypes.ToArray();
            return type => scannerDefinitionTypeArray.Any(def => type.Name.EndsWith(def));
        }

    }
}
