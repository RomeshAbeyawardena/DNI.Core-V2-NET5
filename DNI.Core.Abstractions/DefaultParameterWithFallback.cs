﻿using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public static class ParameterWithFallback
    {
        public static IParameterWithFallback<TParameter, TFallbackParameter> 
            Create<TParameter, TFallbackParameter>(TParameter? parameter, TFallbackParameter fallbackParameter)
            where TParameter : struct
        {
            return new DefaultParameterWithFallback<TParameter, TFallbackParameter>(parameter, fallbackParameter);
        }
    }

    internal class DefaultParameterWithFallback<TParameter, TFallbackParameter> : ParameterWithFallbackBase<TParameter, TFallbackParameter>
        where TParameter : struct
    {
        public DefaultParameterWithFallback(TParameter? parameter, TFallbackParameter fallbackParameter)
            : base(parameter, fallbackParameter)
        {
            
        }

    }
}
