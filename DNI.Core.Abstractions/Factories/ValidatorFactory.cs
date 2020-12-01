using FluentValidation;
using System;

namespace DNI.Core.Abstractions.Factories
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        public ValidatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            var genericValidatorType = typeof(IValidator<>);
            return serviceProvider.GetService(genericValidatorType.MakeGenericType(validatorType)) as IValidator;
        }

        private readonly IServiceProvider serviceProvider;
    }
}
