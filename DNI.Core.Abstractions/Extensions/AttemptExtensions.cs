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
        public static TResult AttemptedResponseResult<T, TResult>(this IAttemptedResponse<T> attempt,
            Func<object, TResult> successResultDelegate,
            Func<IAttempt, TResult> failedResultDelegate)
        {
            if(attempt.Type == RequestQueryType.Multiple)
            {
                if(attempt.AttemptMany != null)
                {
                    if(attempt.AttemptMany.Successful)
                    { 
                        return successResultDelegate(attempt.AttemptMany.Result);
                    }

                    return failedResultDelegate(attempt.AttemptMany);
                }
                throw new NullReferenceException();
            }

            if(attempt.Attempt != null)
            {
                if (attempt.Attempt.Successful)
                {
                    return successResultDelegate(attempt.Attempt.Result);
                }

                return failedResultDelegate(attempt.Attempt);
            }

            throw new NullReferenceException();
        }
    }
}
