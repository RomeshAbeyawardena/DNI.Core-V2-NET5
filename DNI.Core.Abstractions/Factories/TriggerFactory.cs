using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Factories
{
    internal class TriggerFactory : ITriggerFactory
    {
        public TriggerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        IEnumerable<ITriggerEventHandler<T, TEnum>> ITriggerFactory.GetTriggerEventHandlers<T, TEnum>()
        {
            return serviceProvider.GetServices<ITriggerEventHandler<T, TEnum>>();
        }

        private readonly IServiceProvider serviceProvider;
    }
}
