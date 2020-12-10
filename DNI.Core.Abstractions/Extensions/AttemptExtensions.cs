using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Extensions
{
    public static class AttemptExtensions
    {
        public static TResult AttemptedResponseResult<T, TResult>(this IAttempt<T> attempt,
            Func<object, TResult> successResultDelegate,
            Func<IAttempt<T>, TResult> failedResultDelegate)
        {
            if (attempt is not AttemptedResponseBase<T> attemptedResponse)
            {
                return attempt.AttemptedResult(result => successResultDelegate(result), failedResultDelegate);
            }

            if (attempt.Successful)
            {
                if (attemptedResponse.Type == RequestQueryType.Multiple)
                {
                    return successResultDelegate(attemptedResponse.AttemptMany);
                }

                return successResultDelegate(attempt.Result);
            }

            return failedResultDelegate(attempt);
        }
    }
}
