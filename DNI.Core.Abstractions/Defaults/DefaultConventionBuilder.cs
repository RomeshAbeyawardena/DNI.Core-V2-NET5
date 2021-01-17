using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Defaults
{
    public class DefaultConventionBuilder : IConventionBuilder
    {
        public DefaultConventionBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public IConventionBuilder Add<TConvention>(TConvention convention)
            where TConvention : IConvention
        {
            services.AddSingleton<IConvention>(convention);
            return this;
        }

        private readonly IServiceCollection services;
    }
}
