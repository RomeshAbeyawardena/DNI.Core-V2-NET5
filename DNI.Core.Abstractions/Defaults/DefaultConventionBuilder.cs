using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Defaults
{
    public class DefaultConventionBuilder : IConventionBuilder
    {
        public DefaultConventionBuilder()
        {
            Conventions = new List<IConvention>();
        }

        public IEnumerable<IConvention> Conventions { get; }

        public IConventionBuilder Add(IConvention convention)
        {
            Conventions.Append(convention);
            return this;
        }

        public TConvention GetConvention<TConvention>()
            where TConvention : IConvention
        {
            var foundconvention = Conventions.FirstOrDefault(t => t.GetType() == typeof(TConvention));

            if (foundconvention != null && foundconvention is TConvention convention)
            {
                return convention;
            }

            return default;
        }
    }
}
