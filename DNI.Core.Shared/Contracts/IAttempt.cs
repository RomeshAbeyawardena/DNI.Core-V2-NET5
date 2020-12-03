using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// Represents an attempt
    /// </summary>
    public interface IAttempt
    {
        /// <summary>
        /// <para>Gets a list of <see cref="ValidationFailure"/>s</para>
        /// <para>Returns null if the <see cref="IAttempt"/> was successful</para>
        /// </summary>
        IEnumerable<ValidationFailure> ValidationFailures { get; }

        /// <summary>
        /// Gets whether the attempt was successful
        /// </summary>
        bool Successful { get; }

        /// <summary>
        /// <para>Gets the <see cref="System.Exception"/> handled by the attempt, if the <see cref="IAttempt"/> was unsuccessful</para>
        /// <para>Returns null if the <see cref="IAttempt"/> was successful</para>
        /// </summary>
        Exception Exception { get; }
    }

    /// <summary>
    /// Represents an attempt of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAttempt<T> : IAttempt
    {
        /// <summary>
        /// Gets the result if the <see cref="Attempt{T}"/> was successful
        /// </summary>
        T Result { get; }
    }
}
