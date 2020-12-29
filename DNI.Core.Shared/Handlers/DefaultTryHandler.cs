using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Handlers;
using System;

namespace DNI.Core.Shared.Handlers
{
    internal class DefaultTryHandler : DefaultHandler, ITryHandler
    {
        public DefaultTryHandler(Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            Action = action;
            CatchAction = catchAction;
            FinalAction = finalAction;
        }

        public IAttempt AsAttempt()
        {
            try
            {
                Action();
                return Attempt.Success();
            }
            catch (Exception ex)
            {
                InvokeCatchAction(ex);
                return Attempt.Failed(ex);
            }
            finally
            {
                FinalAction?.Invoke(new DefaultFinallyHandler());
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                FinalAction(DefaultFinallyHandler.Create());
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void InvokeCatchAction(Exception exception)
        {
            var defaultCatchHandler = DefaultCatchHandler.Create(exception);
            CatchAction?.Invoke(defaultCatchHandler);
            defaultCatchHandler.GetCatchAction()?.Invoke(exception);
        }

        public Action Action { get; }
        public Action<ICatchHandler> CatchAction { get; }
        public Action<IFinallyHandler> FinalAction { get; }
    }

    internal class DefaultTryHandler<TResult> : DefaultTryHandler, ITryHandler<TResult>
    {
        public DefaultTryHandler(Func<TResult> action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
            : base(() => action(), catchAction, finalAction)
        {
            Action = action;
        }

        public new Func<TResult> Action { get; }

        IAttempt<TResult> ITryHandler<TResult>.AsAttempt()
        {
            try
            {
                var result = Action();
                return Attempt.Success(result);
            }
            catch (Exception exception)
            {
                InvokeCatchAction(exception);
                return Attempt.Failed<TResult>(exception);
            }
            finally
            {
                FinalAction?.Invoke(DefaultFinallyHandler.Create());
            }
        }
    }
}
