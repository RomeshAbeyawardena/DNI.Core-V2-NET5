using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class RequestExceptionHandlerBase<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TException : Exception
    {
        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            OnHandle(request, exception, state);
            return Task.CompletedTask;
        }

        protected RequestExceptionHandlerBase(Action<IDefinition<Type>> configureSupportedExceptionTypes)
        {
            var supportedExceptionTypes = Definition.CreateTypeDefinition(configureSupportedExceptionTypes);
            SupportedExceptionTypes = supportedExceptionTypes;
        }

        protected virtual void OnHandle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state)
        {
            if(!state.Handled 
                && SupportedExceptionTypes.Contains(exception.GetType()))
            { 
                state.SetHandled(CreateInstance(exception));
            }
        }

        protected virtual TResponse CreateInstance(TException exception)
        {
            return (TResponse)Activator.CreateInstance(typeof(TResponse), exception);
        }

        protected IEnumerable<Type> SupportedExceptionTypes { get; }
    }
}
