using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Builders
{
    public interface IConventionBuilder
    {
        IConventionBuilder Add<TConvention>(TConvention convention)
            where TConvention : IConvention;
    }
}
