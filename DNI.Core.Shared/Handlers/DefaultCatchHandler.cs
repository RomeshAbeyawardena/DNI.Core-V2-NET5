using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Handlers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Handlers
{
    internal class DefaultCatchHandler : ICatchHandler
    {
        public DefaultCatchHandler(Exception exception)
        {
            catchDictionary = new ConcurrentDictionary<Type, Action<Exception>>();
            Exception = exception;
        }

        public Exception Exception { get; }

        public ICatchHandler Default(Action<Exception> catchAllAction)
        {
            this.catchAllAction = catchAllAction;
            return this;
        }

        public ICatchHandler When<TException>(Action<Exception> catchAction)
             where TException : Exception
        {
            catchDictionary.TryAdd(typeof(TException), catchAction);
            return this;
        }

        public Action<Exception> GetCatchAction()
        {
            if(catchDictionary.TryGetValue(Exception.GetType(), out var exceptionAction))
            {
                return exceptionAction;
            }

            return catchAllAction;
        } 

        private Action<Exception> catchAllAction;

        private readonly ConcurrentDictionary<Type, Action<Exception>> catchDictionary;
    }
}
