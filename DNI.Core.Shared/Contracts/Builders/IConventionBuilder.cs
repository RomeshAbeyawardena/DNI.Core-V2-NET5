﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Builders
{
    public interface IConventionBuilder
    {
        IConventionBuilder Add(IConvention convention);
        IEnumerable<IConvention> Conventions { get; }

        TConvention GetConvention<TConvention>()
            where TConvention : IConvention;
    }
}
