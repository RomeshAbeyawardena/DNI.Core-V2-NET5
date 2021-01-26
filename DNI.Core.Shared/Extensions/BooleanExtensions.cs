using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class BooleanExtensions
    {
        public static TResult IIf<TResult>(this bool value, TResult onConditionTrue, TResult onConditionFalse = default)
        {
            if (value)
            {
                return onConditionTrue;
            }

            return onConditionFalse;
        }

        public static Task<TResult> IIf<TResult>(this bool value, Task<TResult> onConditionTrue, Task<TResult> onConditionFalse = default)
        {
            return IIf(value, onConditionTrue, onConditionFalse);
        }

        public static Task<TResult> IIf<TResult>(this bool value, Task<TResult> onConditionTrue, TResult onConditionFalse = default)
        {
            return IIf(value, onConditionTrue, Task.FromResult(onConditionFalse));
        }
    }
}
