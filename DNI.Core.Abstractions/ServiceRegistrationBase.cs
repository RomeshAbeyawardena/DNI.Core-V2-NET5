﻿using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

    internal class DefaultServiceRegistration : ServiceRegistrationBase
    {
        public override IServiceCollection RegisterServices(IServiceCollection services)
        {
            return services
                .AddSingleton(Newtonsoft.Json.JsonSerializer.CreateDefault())
                .Scan(scanner => scanner
                    .FromAssemblies(assemblyDefinitions)
                    .AddClasses(classes => classes.Where(DefineScannerFilters(def => def.AddRange(ScanTypes))))
                    .AsImplementedInterfaces());
        }

        private static Func<Type, bool> DefineScannerFilters(Action<IDefinition<string>> definitionTypesAction)
        {
            var scannerDefinitionTypes = Definition.Create<string>();

            definitionTypesAction(scannerDefinitionTypes);
            var scannerDefinitionTypeArray = scannerDefinitionTypes.ToArray();
            return type => scannerDefinitionTypeArray.Any(def => type.Name.EndsWith(def));
        }

        private static IDefinition<Assembly> assemblyDefinitions => 
            GetAssemblies(assembly => assembly.GetAssembly<DefaultServiceRegistration>());

        private static IEnumerable<string> ScanTypes => new []{ "Factory", "Serializer", "Service" };
    }
}
