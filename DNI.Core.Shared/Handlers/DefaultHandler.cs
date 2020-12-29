using DNI.Core.Shared.Contracts.Handlers;
using System;

namespace DNI.Core.Shared.Handlers
{
    /// <inheritdoc cref="IHandler" />
    internal class DefaultHandler : IHandler
    {
        public ITryHandler Try(Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return DefaultTryHandler.Create(action, catchAction, finalAction);
        }

        public ITryHandler<TResult> Try<TResult>(Func<TResult> resultAction, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return DefaultTryHandler<TResult>.Create(resultAction, catchAction, finalAction);
        }
    }
}
