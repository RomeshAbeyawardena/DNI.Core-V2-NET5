using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    public interface IAttempt
    {
        IEnumerable<ValidationFailure> ValidationFailures { get; }
        bool Successful { get; }
        Exception Exception { get; }
    }
    public interface IAttempt<T> : IAttempt
    {
        T Result { get; }
    }
}
