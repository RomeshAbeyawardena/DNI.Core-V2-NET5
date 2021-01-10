using DNI.Core.Shared.Contracts;
using System;

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
