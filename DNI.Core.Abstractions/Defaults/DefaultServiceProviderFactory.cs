using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Core.Abstractions.Defaults
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
