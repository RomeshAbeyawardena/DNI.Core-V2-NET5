using DNI.Core.Abstractions.Defaults;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Builders;
using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Security;

namespace DNI.Core.Abstractions
{
    internal class FluentEncryptionConfiguration : IFluentEncryptionConfiguration
    {
        public IFluentEncryptionConfiguration RegisterModel<T>(Action<IFluentEncryptionConfiguration<T>> action)
        {
            services
                .TryAddSingleton<IFluentEncryptionConfiguration<T>>((s) => {
                    var configuration = new FluentEncryptionConfiguration<T>();
                    action(configuration);
                    return configuration;
                });

            return this;
        }

        public IFluentEncryptionConfiguration RegisterEncryptionClassifications(Action<IEncryptionClassificationOptions> action)
        {

            services.AddSingleton(serviceProvider => {
                var encryptionClassificationOptions = EncryptionClassificationOptions.Create(serviceProvider);
                action(encryptionClassificationOptions);
                return encryptionClassificationOptions.EncryptionClassifications;
            });
            
            return this;
        }

        public IConventionBuilderConfiguration AddConvention<TConvention>(TConvention convention) where TConvention : IConvention
        {
            return conventionBuilderConfiguration.AddConvention(convention);
        }

        public FluentEncryptionConfiguration(IServiceCollection services)
        {
            this.services = services;
            conventionBuilder = new DefaultConventionBuilder(services);
            conventionBuilderConfiguration = new ConventionBuilderConfiguration(conventionBuilder);
        }

        private readonly IConventionBuilder conventionBuilder;
        private readonly IConventionBuilderConfiguration conventionBuilderConfiguration;
        private readonly IServiceCollection services;
    }

    internal class ConventionBuilderConfiguration : IConventionBuilderConfiguration
    {
        public ConventionBuilderConfiguration(IConventionBuilder conventionBuilder)
        {
            ConventionBuilder = conventionBuilder;
        }

        public IConventionBuilder ConventionBuilder { get; }

        public IConventionBuilderConfiguration AddConvention<TConvention>(TConvention convention) where TConvention : IConvention
        {
            ConventionBuilder.Add(convention);

            return this;
        }
    }

    internal class FluentEncryptionConfiguration<T> : IFluentEncryptionConfiguration<T>
    {
        public FluentEncryptionConfiguration()
        {
            options = new List<IFluentEncryptionConfigurationOption<T>>();
        }

        public IFluentEncryptionConfiguration<T> Configure(Expression<Func<T, object>> propertySelector, 
            EncryptionClassification encryptionClassification,
            EncryptionPolicy encryptionPolicy = EncryptionPolicy.RequireEncryption, 
            Func<T, string> getPropertyString = null)
        {
            options.Add(new FluentEncryptionConfigurationOption<T> { 
                PropertyExpression = propertySelector,
                Policy = encryptionPolicy,
                GetPropertyString = getPropertyString,
                Classification = encryptionClassification
            });

            return this;
        }

        public IEnumerable<IFluentEncryptionConfigurationOption<T>> Options => options.ToArray();

        private readonly List<IFluentEncryptionConfigurationOption<T>> options;

    }
}
