using DNI.Core.Shared.Contracts.Handlers;
using System;

namespace DNI.Core.Shared.Handlers
{
    /// <inheritdoc cref="IHandler" />
    internal class DefaultHandler : IHandler
    {
        public ITryHandler Try(Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return new DefaultTryHandler(action, catchAction, finalAction);
        }

        public ITryHandler<TResult> Try<TResult>(Func<TResult> resultAction, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return new DefaultTryHandler<TResult>(resultAction, catchAction, finalAction);
        }
    }
}
