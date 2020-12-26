using DNI.Core.Abstractions.Extensions;
using DNI.Core.Shared.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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

        public abstract void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}
