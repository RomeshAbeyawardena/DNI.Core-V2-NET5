using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DNI.Core.Abstractions
{
    public abstract class ImplementationFactoryBase<TServiceType, TEnum>
        where TEnum : Enum
    {
        protected TServiceType GetServiceType(TEnum enumeration)
        {
            var services = serviceProvider.GetServices<TServiceType>();
            return services.SingleOrDefault(service => getEnumerationAction(service).Equals(enumeration));
        }

        public ImplementationFactoryBase(IServiceProvider serviceProvider, Func<TServiceType, TEnum> getEnumerationAction)
        {
            this.serviceProvider = serviceProvider;
            this.getEnumerationAction = getEnumerationAction;
        }

        private readonly IServiceProvider serviceProvider;
        private readonly Func<TServiceType, TEnum> getEnumerationAction;
    }
}
