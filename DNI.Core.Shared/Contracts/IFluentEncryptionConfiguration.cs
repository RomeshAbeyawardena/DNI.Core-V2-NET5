using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IFluentEncryptionConfiguration<T>
    {
        IFluentEncryptionConfiguration<T> Configure(
            Expression<Func<T, object>> propertySelector, 
            EncryptionClassification encryptionClassification,
            EncryptionPolicy encryptionPolicy = EncryptionPolicy.RequireEncryption, 
            Func<T, string> getPropertyString = default);

        IEnumerable<IFluentEncryptionConfigurationOption<T>> Options { get; }
    }

    public interface IFluentEncryptionConfigurationOption<T>
    {
        Expression<Func<T, object>> PropertyExpression { get; }
        EncryptionClassification Classification { get; }
        EncryptionPolicy Policy { get; }
        Func<T, string> GetPropertyString { get; }
    }
}
