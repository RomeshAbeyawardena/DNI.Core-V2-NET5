using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITryHandler : IHandler, IDisposable
    {
        /// <summary>
        /// Creates an attempt object based on this TryHandler
        /// </summary>
        /// <returns>An instance of an <see cref="IAttempt"/></returns>
        IAttempt AsAttempt();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IAttempt> AsAttemptAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Represents the action that the handler will attempt
        /// </summary>
        Action Action { get; }

        /// <summary>
        /// 
        /// </summary>
        Func<CancellationToken, Task> ActionAsync { get; }
        /// <summary>
        /// Represents the action that gets triggered when <see cref="Action"/> throws an exception
        /// </summary>
        Action<ICatchHandler> CatchAction { get; }

        /// <summary>
        /// Represents the action that gets fired each time irrespective of success or failure
        /// </summary>
        Action<IFinallyHandler> FinalAction { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface ITryHandler<TResult> : ITryHandler
    {
        /// <summary>
        /// Creates an attempt object based on this TryHandler
        /// </summary>
        /// <returns>An instance of an <see cref="IAttempt{T}"/></returns>
        new IAttempt<TResult> AsAttempt();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        new Task<IAttempt<TResult>> AsAttemptAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        new Func<CancellationToken, Task<TResult>> ActionAsync { get; }

        /// <summary>
        /// Represents the action that the handler will attempt
        /// </summary>
        new Func<TResult> Action { get; }
    }
}
