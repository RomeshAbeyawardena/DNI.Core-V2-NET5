using DNI.Core.Abstractions.Extensions;
using DNI.Core.Shared.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Core.Abstractions.Bootstrap
{
    public abstract class Startup<TServiceRegistration>
        where TServiceRegistration : IServiceRegistration
    {
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .RegisterServices<TServiceRegistration>()
                .AddDistributedMemoryCache();
        }

        public IFluentEncryptionConfiguration ConfigureEncryption<T>(IServiceCollection services, 
            Action<IFluentEncryptionConfiguration<T>> configurationAction)
        {
            return services.RegisterModelForFluentEncryption(configurationAction);
        } 

        public abstract void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}
