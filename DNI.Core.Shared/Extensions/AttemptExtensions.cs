using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class AttemptExtensions
    {
        public static TResult AttemptedResult<T, TResult>(this IAttempt<T> attempt,
            Func<T, TResult> successResultDelegate,
            Func<IAttempt<T>, TResult> failedResultDelegate)
        {
            if (attempt.Successful)
            {
                return successResultDelegate(attempt.Result);
            }

            return failedResultDelegate(attempt);
        }
    }
}
