using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Services
{
    public class DefaultModelEncryptionService<T> : IModelEncryptionService<T>
    {
        public DefaultModelEncryptionService(IFluentEncryptionConfiguration<T> fluentEncryptionConfiguration,
            IEncryptionClassificationFactory encryptionClassificationFactory,
            IHashServiceFactory hashServiceFactory,
            IEncryptionFactory encryptionFactory)
        {
            this.encryptionFactory = encryptionFactory;
            this.fluentEncryptionConfiguration = fluentEncryptionConfiguration;
            this.encryptionClassificationFactory = encryptionClassificationFactory;
            this.hashServiceFactory = hashServiceFactory;
            modelExpressionVisitor = new ModelExpressionVisitor();
        }

        public void Encrypt(T model)
        {
            ProcessOptions(fluentEncryptionConfiguration, model, (encryptionOptions, encryptionService, property, value) => {
                if(value != null)
                { 
                    property.SetValue(model, encryptionService.Encrypt(value.ToString(), encryptionOptions));
                }
            });
        }

        private void ProcessOptions(
            IFluentEncryptionConfiguration<T> configuration, 
            T model,
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

                processAction(encryptionOptions, encryptionService, property, value);
            }
            
        }

        public void Decrypt(T model)
        {
            ProcessOptions(fluentEncryptionConfiguration, model, (encryptionOptions, encryptionService, property, value) => {
                if(value != null)
                { 
                    property.SetValue(model, encryptionService.Decrypt(value.ToString(), encryptionOptions));
                }
            });
        }

        internal class ModelExpressionVisitor : ExpressionVisitor
        {
            public string GetLastVisitedMember(Expression exp)
            {
                Visit(exp);
                return memberName;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                memberName = node.Member.Name;
                return base.VisitMember(node);
            }

            private string memberName;
        }

        private readonly IEncryptionFactory encryptionFactory;
        private readonly ModelExpressionVisitor modelExpressionVisitor;
        private readonly IFluentEncryptionConfiguration<T> fluentEncryptionConfiguration;
        private readonly IEncryptionClassificationFactory encryptionClassificationFactory;
        private readonly IHashServiceFactory hashServiceFactory;
    }
}
