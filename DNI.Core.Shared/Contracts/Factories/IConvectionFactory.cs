using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface IConvectionFactory
    {
        TConvection GetConvection<TConvection>();
        IEnumerable<IConvention> Conventions { get; }
    }
}
