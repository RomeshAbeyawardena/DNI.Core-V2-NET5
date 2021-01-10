using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public class DefaultServiceProviderFactory<T> : IServiceProviderFactory<T>
    {
        public T CreateBuilder(IServiceCollection services)
        {
            return default;
        }

        public IServiceProvider CreateServiceProvider(T containerBuilder)
        {
            return services.BuildServiceProvider();
        }

        public DefaultServiceProviderFactory(IServiceCollection services)
        {
            this.services = services;
        }

        private readonly IServiceCollection services;
    }
}
