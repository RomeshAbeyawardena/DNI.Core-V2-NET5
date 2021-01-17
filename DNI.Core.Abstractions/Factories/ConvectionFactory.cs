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
    internal class ConvectionFactory : IConvectionFactory
    {
        public IEnumerable<IConvention> Conventions { get; }

        public TConvection GetConvection<TConvection>()
        {
            var convection = Conventions.FirstOrDefault(c => c.GetType() is TConvection);

            if(convection != null)
            {
                return (TConvection)convection;
            }

            return default;
        }

        public ConvectionFactory(IServiceProvider serviceProvider)
        {
            Conventions = serviceProvider.GetServices<IConvention>();
        }
    }
}
