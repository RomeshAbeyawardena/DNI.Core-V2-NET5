using DNI.Core.Shared.Contracts.Handlers;
using System;

namespace DNI.Core.Shared.Handlers
{
    /// <inheritdoc cref="IHandler" />
    internal class DefaultHandler : IHandler
    {
        public ITryHandler Try(Action action, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return IsCatchOrFinallyHandlerNull 
                ? DefaultTryHandler.Create(CatchHandler, FinallyHandler, action, catchAction, finalAction) 
                : DefaultTryHandler.Create(action, catchAction, finalAction);
        }

        public ITryHandler<TResult> Try<TResult>(Func<TResult> resultAction, Action<ICatchHandler> catchAction, Action<IFinallyHandler> finalAction)
        {
            return IsCatchOrFinallyHandlerNull
                ? DefaultTryHandler<TResult>.Create(CatchHandler, FinallyHandler, resultAction, catchAction, finalAction) 
                : DefaultTryHandler<TResult>.Create(resultAction, catchAction, finalAction);
        }

        public DefaultHandler()
        {

        }

        internal DefaultHandler(ICatchHandler catchHandler, IFinallyHandler finallyHandler)
        {
            CatchHandler = catchHandler;
            FinallyHandler = finallyHandler;
        }

        protected ICatchHandler CatchHandler { get; set; }
        protected IFinallyHandler FinallyHandler { get; set; }

        private bool IsCatchOrFinallyHandlerNull => CatchHandler != null ||
                FinallyHandler != null;
    }
}
