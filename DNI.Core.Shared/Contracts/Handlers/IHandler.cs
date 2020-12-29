using System;

namespace DNI.Core.Shared.Contracts.Handlers
{
    /// <summary>
    /// Represents a helper to create a try, catch and finally handler
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Creates an instance of a try, catch and finally handler
        /// </summary>
        /// <param name="action"></param>
        /// <param name="catchAction"></param>
        /// <param name="finalAction"></param>
        /// <returns></returns>
        ITryHandler Try(Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction);
        /// <summary>
        /// Creates an instance of a try, catch and finally handler
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="resultAction"></param>
        /// <param name="catchAction"></param>
        /// <param name="finalAction"></param>
        /// <returns></returns>
        ITryHandler<TResult> Try<TResult>(Func<TResult> resultAction, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction);
    }
}
