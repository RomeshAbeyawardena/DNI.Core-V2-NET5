using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using System;
using System.Linq.Expressions;
using System.Net.Security;

namespace DNI.Core.Shared.Options
{
    public class FluentEncryptionConfigurationOption<T> : IFluentEncryptionConfigurationOption<T>
    {
        public Expression<Func<T, object>> PropertyExpression { get; set; }

        public EncryptionPolicy Policy { get; set; }

        public Func<T, string> GetPropertyString { get; set; }

        public EncryptionClassification Classification { get; set; }
    }
}
