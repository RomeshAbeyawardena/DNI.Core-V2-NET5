using DNI.Core.Abstractions.Defaults;
using DNI.Core.Shared;
using DNI.Core.Shared.Attributes;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DNI.Core.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterFileCacheDependency(this IServiceCollection services,
            Action<ISettingConfigurator<FileCacheDependencyOptions>, bool> settingConfiguration)
        {
            return RegisterFileCacheDependency(services, (serviceProvider, configure) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var fileCacheDependencyOptionsConfigurator = new DefaultSettingConfigurator<FileCacheDependencyOptions>(configuration);
                
                settingConfiguration.Invoke(fileCacheDependencyOptionsConfigurator, true);
                fileCacheDependencyOptionsConfigurator.ConfigureSettings(configure);
            });
        }

        public static IServiceCollection RegisterFileCacheDependency(this IServiceCollection services)
        {
            return RegisterFileCacheDependency(services, (configure, b) =>
            {
                configure.ConfigureSection(s => s.GetSection("CacheDependency"))
                    .Configure(s => s.DependencyFile, t => t.GetValue<string>("DependencyFile"))
                    .Configure(s => s.ElapsedPeriod, t => t.GetValue<TimeSpan>("ElapsedPeriod")); 
            });
        }


        public static IServiceCollection RegisterFileCacheDependency(this IServiceCollection services,
            Action<FileCacheDependencyOptions> configAction)
        {
            var options = new FileCacheDependencyOptions();
            configAction(options);
            return services
                .AddSingleton<ICacheDependencyOptions>(options)
                .AddSingleton<ICacheDependency, FileCacheDependency>();
        }

        public static IServiceCollection RegisterFileCacheDependency(this IServiceCollection services,
            Action<IServiceProvider, FileCacheDependencyOptions> configAction)
        {
            var options = new FileCacheDependencyOptions();

            return services
                .AddSingleton<ICacheDependencyOptions>(s =>
                {
                    configAction(s, options);
                    return options;
                })
                .AddSingleton<ICacheDependency, FileCacheDependency>();
        }

        public static IFluentEncryptionConfiguration RegisterEncryptionClassifications(this IServiceCollection services,
            Action<IEncryptionClassificationOptions> action)
        {
            return new FluentEncryptionConfiguration(services)
                .RegisterEncryptionClassifications(action);
        }

        public static IFluentEncryptionConfiguration RegisterModelForFluentEncryption<T>(this IServiceCollection services,
            Action<IFluentEncryptionConfiguration<T>> action)
        {
            return new FluentEncryptionConfiguration(services)
                .RegisterModel(action);
        }

        public static IServiceCollection RegisterServices<TServiceRegistration>(this IServiceCollection services,
            bool registerInternalServices = true)
            where TServiceRegistration : IServiceRegistration
        {
            var serviceRegistration = Activator.CreateInstance<TServiceRegistration>();
            if (registerInternalServices)
            {
                DefaultServiceRegistration.Create().RegisterServices(services);
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
            .AddClasses(classFilter => classFilter
            .WithoutAttribute<IgnoreScanningAttribute>()
            .Where(DefineScannerFilters(scanTypeDefinitionAction)))
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
