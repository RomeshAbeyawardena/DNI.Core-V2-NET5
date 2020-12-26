using DNI.Core.Abstractions.Extensions;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
