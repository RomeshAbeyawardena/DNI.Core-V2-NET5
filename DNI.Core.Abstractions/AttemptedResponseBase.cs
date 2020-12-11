using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace DNI.Core.Abstractions
{
    public abstract class AttemptedResponseBase<T> : IAttemptedResponse<T>
    {
        protected AttemptedResponseBase (
            IAttempt<T> attempt = default,
            IAttempt<IEnumerable<T>> attemptMany = default,
            RequestQueryType type = RequestQueryType.None)
        {
            Type = type;
            Attempt = attempt;
            AttemptMany = attemptMany;
            Type = type;
        }

        protected AttemptedResponseBase (IAttempt<T> attempt, RequestQueryType type = RequestQueryType.Single)
            : this(attempt, null, type)
        {

        }

        protected AttemptedResponseBase (IAttempt<IEnumerable<T>> attemptMany, RequestQueryType type = RequestQueryType.Multiple)
            : this(null, attemptMany, type)
        {

        }

        protected AttemptedResponseBase (T result, RequestQueryType type = RequestQueryType.Single)
            : this(Shared.Attempt.Success(result), type)
        {

        }

        protected AttemptedResponseBase (IEnumerable<T> result, RequestQueryType type = RequestQueryType.Single)
            : this(null, Shared.Attempt.Success(result), type)
        {

        }

        protected AttemptedResponseBase (Exception exception, IEnumerable<ValidationFailure> validationFailures = default)
            : this(Shared.Attempt.Failed<T>(exception, validationFailures), RequestQueryType.None)
        {

        }

        public RequestQueryType Type { get; }
        public IAttempt<T> Attempt { get; }
        public IAttempt<IEnumerable<T>> AttemptMany { get; }
        IAttempt IAttemptedResponse.Attempt => Attempt ?? (IAttempt)AttemptMany;
    }
}
