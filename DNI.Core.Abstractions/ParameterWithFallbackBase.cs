using DNI.Core.Shared.Contracts;
using System;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class ParameterWithFallbackBase<TParameter, TFallbackParameter> 
        : IParameterWithFallback<TParameter, TFallbackParameter>
        where TParameter : struct
    {
        public TParameter? Parameter { get; }

        public TFallbackParameter FallbackParameter { get; }

        public bool HasValue => Parameter.HasValue || FallbackParameter != null;

        public void Invoke(Action<TParameter> defaultAction, Action<TFallbackParameter> fallbackAction)
        {
            if (Parameter.HasValue)
            {
                defaultAction(Parameter.Value);
            }
            else
            {
                fallbackAction(FallbackParameter);
            }
        }

        public TResult Invoke<TResult>(Func<TParameter, TResult> defaultAction, Func<TFallbackParameter, TResult> fallbackAction)
        {
            if (Parameter.HasValue)
            {
                return defaultAction(Parameter.Value);
            }

            return fallbackAction(FallbackParameter);
        }

        public Task<TResult> Invoke<TResult>(Func<TParameter, Task<TResult>> defaultAction, Func<TFallbackParameter, Task<TResult>> fallbackAction)
        {
            if (Parameter.HasValue)
            {
                return defaultAction(Parameter.Value);
            }

            return fallbackAction(FallbackParameter);
        }

        protected ParameterWithFallbackBase(TParameter? parameter, TFallbackParameter fallbackParameter)
        {
            Parameter = parameter;
            FallbackParameter = fallbackParameter;
        }
    }
}
