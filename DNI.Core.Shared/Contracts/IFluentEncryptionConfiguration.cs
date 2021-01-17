using DNI.Core.Shared.Contracts.Builders;
using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Security;

namespace DNI.Core.Shared.Contracts
{
    public interface IFluentEncryptionConfiguration
    {
        IFluentEncryptionConfiguration RegisterModel<T>(Action<IFluentEncryptionConfiguration<T>> action);
        IFluentEncryptionConfiguration RegisterEncryptionClassifications(Action<IEncryptionClassificationOptions> action);
        IConventionBuilderConfiguration AddConvention<TConvention>(TConvention convention)
            where TConvention : IConvention;
    }

    public interface IConventionBuilderConfiguration
    {
        IConventionBuilderConfiguration AddConvention<TConvention>(TConvention convention)
            where TConvention : IConvention;

        IConventionBuilder ConventionBuilder { get; }
    }

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
