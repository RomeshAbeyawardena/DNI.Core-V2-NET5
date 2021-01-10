using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    internal class DefaultAppHost<TStartup> : IHostBuilder
        where TStartup : class, IHost
    {
        public IDictionary<object, object> Properties => throw new NotImplementedException();

        public IHost Build()
        {
            throw new NotImplementedException();
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(hostBuilderContext, configurationBuilder);
            return this;
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(hostBuilderContext, default);
            return this;
        }

        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(configurationBuilder);
            return this;
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            configureDelegate?.Invoke(hostBuilderContext, services);
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            var spf = new DefaultServiceProviderFactory<TContainerBuilder>(services);
            factory?.Invoke(hostBuilderContext);
            return this;
        }

        public DefaultAppHost()
            : this(new ServiceCollection())
        {
            
        }

        public DefaultAppHost(IServiceCollection services)
        {
            this.services = services;
            configurationBuilder = new DefaultConfigurationBuilder();
            hostBuilderContext = new HostBuilderContext(Properties);
        }


        private readonly IServiceCollection services;
        private readonly IConfigurationBuilder configurationBuilder;
        private readonly HostBuilderContext hostBuilderContext;
    }
}
