using DNI.Core.Shared.Contracts;
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
        public IFluentEncryptionConfiguration Configure<T>(Action<IFluentEncryptionConfiguration<T>> action)
        {
            services
                .TryAddSingleton<IFluentEncryptionConfiguration<T>>((s) => {
                    var configuration = new FluentEncryptionConfiguration<T>();
                    action(configuration);
                    return configuration;
                });

            return this;
        }

        public FluentEncryptionConfiguration(IServiceCollection services)
        {
            this.services = services;
        }

        private readonly IServiceCollection services;
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
