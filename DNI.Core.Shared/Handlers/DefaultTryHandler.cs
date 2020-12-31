using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Handlers;
using System;

namespace DNI.Core.Shared.Handlers
{
    /// <inheritdoc cref="ITryHandler" />
    internal class DefaultTryHandler : DefaultHandler, ITryHandler
    {
        public static ITryHandler Create(Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return new DefaultTryHandler(action, catchAction, finalAction);
        }

        internal static ITryHandler Create(ICatchHandler catchHandler, IFinallyHandler finallyHandler, 
            Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return new DefaultTryHandler(catchHandler, finallyHandler, action, catchAction, finalAction);
        }

        private bool IsFinalActionInvoked { get; set; }
        public Action Action { get; }
        public Action<ICatchHandler> CatchAction { get; }
        public Action<IFinallyHandler> FinalAction { get; }

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
                InvokeFinalAction();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsFinalActionInvoked)
            {
                InvokeFinalAction();
            }
        }

        protected void InvokeCatchAction(Exception exception)
        {
            var defaultCatchHandler = CatchHandler ?? DefaultCatchHandler.Create(exception);
            CatchAction?
                .Invoke(defaultCatchHandler);
            defaultCatchHandler.GetCatchAction()?
                .Invoke(exception);
        }

        protected void InvokeFinalAction()
        {
            FinalAction?.Invoke(FinallyHandler ?? DefaultFinallyHandler.Create());
            IsFinalActionInvoked = true;
        }

        protected DefaultTryHandler(Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            Action = action;
            CatchAction = catchAction;
            FinalAction = finalAction;
        }

        protected DefaultTryHandler(ICatchHandler catchHandler, IFinallyHandler finallyHandler, 
            Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
            : this(action, catchAction, finalAction)

        {
            CatchHandler = catchHandler;
            FinallyHandler = finallyHandler;
        }
    }

    /// <see cref="ITryHandler{TResult}"/>
    internal class DefaultTryHandler<TResult> : DefaultTryHandler, ITryHandler<TResult>
    {
        public static ITryHandler<TResult> Create(Func<TResult> action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return new DefaultTryHandler<TResult>(action, catchAction, finalAction);
        }

        internal static ITryHandler<TResult> Create(ICatchHandler catchHandler, IFinallyHandler finallyHandler, 
            Func<TResult> action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return new DefaultTryHandler<TResult>(catchHandler, finallyHandler, action, catchAction, finalAction);
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
                InvokeFinalAction();
            }
        }

        protected DefaultTryHandler(Func<TResult> action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
            : base(() => action(), catchAction, finalAction)
        {
            Action = action;
        }

        protected DefaultTryHandler(ICatchHandler catchHandler, IFinallyHandler finallyHandler, 
            Func<TResult> action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
            : base(catchHandler, finallyHandler, () => action(), catchAction, finalAction)
        {
            Action = action;
        }
    }
}
