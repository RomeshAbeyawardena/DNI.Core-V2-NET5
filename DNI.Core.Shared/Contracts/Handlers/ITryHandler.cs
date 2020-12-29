using System;

namespace DNI.Core.Shared.Contracts.Handlers
{
    public interface ITryHandler : IHandler
    {
        IAttempt AsAttempt();
        Action Action { get; }
        Action<ICatchHandler> CatchAction { get; }
        Action<IFinallyHandler> FinalAction { get; }
    }

    public interface ITryHandler<TResult> : ITryHandler
    {
        new IAttempt<TResult> AsAttempt();
        new Func<TResult> Action { get; }
    }
}
