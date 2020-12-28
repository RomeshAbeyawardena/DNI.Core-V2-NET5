using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var defaultCatchHandler = new DefaultCatchHandler(ex);
                InvokeCatchAction(defaultCatchHandler, ex);
                return Attempt.Failed(ex);
            }
            finally
            {
                FinalAction?.Invoke(new DefaultFinallyHandler());
            }
        }

        protected void InvokeCatchAction(ICatchHandler catchHandler, Exception exception)
        {
            CatchAction?.Invoke(catchHandler);
            catchHandler.GetCatchAction()?.Invoke(exception);
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
            catch (Exception ex)
            {
                var defaultCatchHandler = new DefaultCatchHandler(ex);
                InvokeCatchAction(defaultCatchHandler, ex);
                return Attempt.Failed<TResult>(ex);
            }
            finally
            {
                FinalAction?.Invoke(new DefaultFinallyHandler());
            }
        }
    }
}
