using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IParameterWithFallback<TParameter, TFallbackParameter>
        where TParameter : struct
    {
        TParameter? Parameter { get;}
        TFallbackParameter FallbackParameter { get; }

        void Invoke(
            Action<TParameter> defaultAction, 
            Action<TFallbackParameter> fallbackAction);

        TResult Invoke<TResult>(
            Func<TParameter, TResult> defaultAction, 
            Func<TFallbackParameter, TResult> fallbackAction);

        Task<TResult> Invoke<TResult>(
            Func<TParameter, Task<TResult>> defaultAction, 
            Func<TFallbackParameter, Task<TResult>> fallbackAction);
    }
}
