using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Core.Abstractions
{
    public abstract class ExceptionHandlerBase<TRequest, TResponse> : RequestExceptionHandler<TRequest, TResponse>
        where TRequest : IActionRequest<TResponse>
    {
        protected ExceptionHandlerBase(Action<IDefinition<Type>> configureSupportedExceptionTypes)
        {
            var supportedExceptionTypes = Definition.CreateTypeDefinition(configureSupportedExceptionTypes);
            SupportedExceptionTypes = supportedExceptionTypes;
        }

        protected override void Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state)
        {
            OnHandle(request, exception, state);
        }

        protected virtual void OnHandle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state)
        {
            if(SupportedExceptionTypes.Contains(exception.GetType()))
            { 
                state.SetHandled(CreateInstance(exception));
            }
        }

        protected virtual TResponse CreateInstance(Exception exception)
        {
            return (TResponse)Activator.CreateInstance(typeof(TResponse), exception);
        }

        protected IEnumerable<Type> SupportedExceptionTypes { get; }
    }
}
