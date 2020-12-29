using System;

namespace DNI.Core.Shared.Contracts.Handlers
{
    public interface IHandler
    {
        ITryHandler Try(Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction);
        ITryHandler<TResult> Try<TResult>(Func<TResult> resultAction, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction);
    }
}
