using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class ExceptionHandlerBase<TRequest, TResponse> : RequestExceptionHandler<TRequest, TResponse>
        where TRequest : IActionRequest<TResponse>
        where TResponse : IAttemptedResponse
    {
        protected ExceptionHandlerBase(IExceptionResourceFactory exceptionResourceFactory)
        {
            ExceptionResourceFactory = exceptionResourceFactory;
        }

        protected override void Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state)
        {
            OnHandle(request, exception, state);
        }

        protected virtual void OnHandle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state)
        {
            state.SetHandled(CreateInstance(exception));
        }

        protected virtual TResponse CreateInstance(Exception exception)
        {
            return (TResponse)Activator.CreateInstance(typeof(TResponse), exception);
        }

        protected IExceptionResourceFactory ExceptionResourceFactory { get; }
    }
}
