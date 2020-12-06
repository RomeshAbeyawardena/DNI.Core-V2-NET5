using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    internal class FluentEncryptionConfiguration<T> : IFluentEncryptionConfiguration<T>
    {
        public FluentEncryptionConfiguration()
        {
            options = new List<IFluentEncryptionConfigurationOption<T>>();
        }

        public IFluentEncryptionConfiguration<T> Configure(Expression<Func<T, object>> propertySelector, EncryptionPolicy encryptionPolicy = EncryptionPolicy.RequireEncryption, Func<T, string> getPropertyString = null)
        {
            options.Add(new FluentEncryptionConfigurationOption<T> { 
                PropertyExpression = propertySelector,
                Policy = encryptionPolicy,
                GetPropertyString = getPropertyString
            });

            return this;
        }

        public IEnumerable<IFluentEncryptionConfigurationOption<T>> Options => options.ToArray();

        private readonly List<IFluentEncryptionConfigurationOption<T>> options;

    }
}
