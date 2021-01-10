using System;

namespace DNI.Core.Shared.Contracts.Handlers
{
    /// <summary>
    /// Represents a catch handler used to handle a particular exception being thrown
    /// </summary>
    public interface ICatchHandler
    {
        /// <summary>
        /// Gets the exception the catch handler handles
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// Describes a catch handler this instance is capable of handling
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="catchAction"></param>
        /// <returns></returns>
        ICatchHandler When<TException>(Action<Exception> catchAction) where TException : Exception;

        /// <summary>
        /// Describes a default catch handler that will be triggered if a suitable catch handler for an exception can not be found.
        /// </summary>
        /// <param name="catchAllAction"></param>
        /// <returns></returns>
        ICatchHandler Default(Action<Exception> catchAllAction);

        /// <summary>
        /// Describes how a suitable catch handler will be determined based on an exception.
        /// </summary>
        /// <returns></returns>
        Action<Exception> GetCatchAction();
    }
}
