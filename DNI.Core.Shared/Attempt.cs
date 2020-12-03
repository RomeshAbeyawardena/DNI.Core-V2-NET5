using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using DNI.Core.Shared.Contracts;

namespace DNI.Core.Shared
{
    public class Attempt : IAttempt
    {
        public static IAttempt Success()
        {
            return new Attempt();
        }

        public static IAttempt Failed(Type type, Exception exception, IEnumerable<ValidationFailure> validationFailures)
        {
            var genericAttempt = typeof(Attempt<>);

            return (IAttempt) Activator.CreateInstance(genericAttempt.MakeGenericType(type), exception, validationFailures);
        }

        public static IAttempt<T> Success<T>(T result)
        {
            return new Attempt<T>(result);
        }

        public static IAttempt Validate(ValidationResult validationResult)
        {
            return new Attempt(validationResult);
        }

        public static IAttempt Failed(Exception exception, IEnumerable<ValidationFailure> validationFailures = default)
        {
            return new Attempt(exception, validationFailures);
        }

        public static IAttempt<T> Validate<T>(ValidationResult validationResult)
        {
            return new Attempt<T>(validationResult);
        }

        public static IAttempt<T> Failed<T>(Exception exception, IEnumerable<ValidationFailure> validationFailures = default)
        {
            return new Attempt<T>(exception, validationFailures);
        }

        public IEnumerable<ValidationFailure> ValidationFailures { get; protected set; }

        public bool Successful { get; protected set; }

        public Exception Exception { get; protected set; }

        protected Attempt()
        {
            Successful = true;
        }

        protected Attempt(ValidationResult validationResult)
        {
            Successful = validationResult.IsValid;
            ValidationFailures = validationResult.Errors;
            Exception = Successful ? null : new ValidationException(ValidationFailures);
        }

        protected Attempt(Exception exception, IEnumerable<ValidationFailure> validationFailures)
        {
            Exception = exception;
            ValidationFailures = validationFailures;
        }
    }

    /// <inheritdoc cref="IAttempt{T}"/>
    class Attempt<T> : Attempt, IAttempt<T>
    {
        public Attempt(IAttempt attempt, T result = default)
        {
            Successful = attempt.Successful;
            Exception = attempt.Exception;
            ValidationFailures = attempt.ValidationFailures;
            Result = result;
        }

        public T Result { get; }

        public Attempt(Exception exception, IEnumerable<ValidationFailure> validationFailures)
            : base(exception, validationFailures)
        {

        }

        internal Attempt(ValidationResult validationResult)
            : base(validationResult)
        {

        }

        internal Attempt(T result)
           : base()
        {
            Result = result;
        }
    }
}
