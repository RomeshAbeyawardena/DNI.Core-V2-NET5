﻿using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.ExpressionVisitors;
using DNI.Core.Shared.Options;
using System;
using System.Net.Security;
using System.Reflection;

namespace DNI.Core.Abstractions.Services
{
    internal class DefaultModelEncryptionService<T> : IModelEncryptionService<T>
    {
        public DefaultModelEncryptionService(IFluentEncryptionConfiguration<T> fluentEncryptionConfiguration,
            IEncryptionClassificationFactory encryptionClassificationFactory,
            IEncryptionFactory encryptionFactory)
        {
            this.encryptionFactory = encryptionFactory;
            this.fluentEncryptionConfiguration = fluentEncryptionConfiguration;
            this.encryptionClassificationFactory = encryptionClassificationFactory;
            modelExpressionVisitor = new ModelExpressionVisitor();
        }

        public void Encrypt(T model)
        {
            ProcessOptions(fluentEncryptionConfiguration, model, true, (encryptionOptions, encryptionService, property, value) => {
                if(value != null)
                { 
                    property.SetValue(model, encryptionService.Encrypt(value.ToString(), encryptionOptions));
                }
            });
        }

        private void ProcessOptions(
            IFluentEncryptionConfiguration<T> configuration, 
            T model, bool getPropertyString,
            Action<EncryptionOptions, IEncryptionService, PropertyInfo, object> processAction)
        {
            var modelType = typeof(T);
             foreach (var option in configuration.Options)
            {
                if(option.Policy == EncryptionPolicy.NoEncryption)
                {
                    continue;
                }

                var encryptionOptions = encryptionClassificationFactory.GetEncryptionOptions(option.Classification);

                var encryptionService = encryptionFactory.GetEncryptionService(encryptionOptions.AlgorithmName);

                var memberName = modelExpressionVisitor.GetLastVisitedMember(option.PropertyExpression.Body);

                var property = modelType.GetProperty(memberName);

                var value = property.GetValue(model);

                var val = getPropertyString 
                    ? option.GetPropertyString?.Invoke(model) 
                    : string.Empty;

                processAction(encryptionOptions, encryptionService, property, string.IsNullOrEmpty(val) ? value : val);
            }
            
        }

        public void Decrypt(T model)
        {
            ProcessOptions(fluentEncryptionConfiguration, model, false, (encryptionOptions, encryptionService, property, value) => {
                if(value != null)
                { 
                    property.SetValue(model, encryptionService.Decrypt(value.ToString(), encryptionOptions));
                }
            });
        }

        private readonly IEncryptionFactory encryptionFactory;
        private readonly ModelExpressionVisitor modelExpressionVisitor;
        private readonly IFluentEncryptionConfiguration<T> fluentEncryptionConfiguration;
        private readonly IEncryptionClassificationFactory encryptionClassificationFactory;
    }
}