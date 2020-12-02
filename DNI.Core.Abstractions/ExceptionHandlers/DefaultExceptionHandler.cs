using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using FluentValidation.Results;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.ExceptionHandlers
{
    public class DefaultExceptionHandler<TRequest, TResponse, TException> : RequestExceptionHandler<TRequest, TResponse, TException>
        where TRequest : IActionRequest<TResponse>
        where TResponse : IAttemptedResponse
        where TException : Exception
    {
        protected override void Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state)
        {
            var responseType = typeof(TResponse);
            var genericArguments = responseType.BaseType.GetGenericArguments();
            var attempt = Attempt.Failed(genericArguments.FirstOrDefault(), exception, Array.Empty<ValidationFailure>());
            //#if !DEBUG
            state.SetHandled((TResponse)Activator.CreateInstance(typeof(TResponse), attempt)); 
            //#endif
        }
    }
}
