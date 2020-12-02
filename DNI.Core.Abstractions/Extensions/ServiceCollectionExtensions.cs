using DNI.Core.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices<TServiceRegistration>(this IServiceCollection services, bool registerInternalServices = true)
            where TServiceRegistration : IServiceRegistration
        {
            var serviceRegistration = Activator.CreateInstance<TServiceRegistration>();
            if(registerInternalServices)
            { 
                new DefaultServiceRegistration().RegisterServices(services);
            }
            serviceRegistration.RegisterServices(services);
            return services;
        }

    }
}
