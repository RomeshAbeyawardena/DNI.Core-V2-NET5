using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared;

namespace DNI.Core.Abstractions
{
    public abstract class ActionRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IActionRequest<TResponse>
    {
        protected ActionRequestHandler(
            ICacheServiceFactory cacheServiceFactory,
            IValidatorFactory validatorFactory)
            : this(cacheServiceFactory, validatorFactory, null, null)
        {
            
        }

        protected ActionRequestHandler(
            ICacheServiceFactory cacheServiceFactory,
            IValidatorFactory validatorFactory,
            Action<ISwitch<RequestAction, Func<TRequest, CancellationToken, Task<TResponse>>>> action = default, 
            Action<ISwitch<RequestAction, Func<TRequest, CancellationToken, Task<IAttempt>>>> validationAction = default)
        {
            CacheServiceFactory = cacheServiceFactory;
            ValidatorFactory = validatorFactory;
            RequestActions = Switch.Create(action);
            RequestValidation = Switch.Create(validationAction);
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (!RequestActions.TryGetValue(request.Action, out var requestAction))
            {
                throw new NotSupportedException($"Unable to find an action for {request.Action}");
            }

            if(RequestValidation.TryGetValue(request.Action, out var requestValidationDelegate))
            {
                var attempt = await requestValidationDelegate.Invoke(request, cancellationToken);

                if (!attempt.Successful)
                {
                    if (attempt.ValidationFailures != null && attempt.ValidationFailures.Any())
                    {
                        throw new ValidationException(attempt.ValidationFailures);
                    }

                    throw new ArgumentException("Request validation failed", nameof(request));
                }
                
            }

            return await requestAction?.Invoke(request, cancellationToken);
        }

        protected Task<ValidationResult> ValidateAsync<T>(T model, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext<T>(model);
            return ValidatorFactory.GetValidator<T>().ValidateAsync(validationContext, cancellationToken);
        }
        
        protected ICacheService GetCacheService(CacheServiceType cacheServiceType)
        {
            return CacheServiceFactory.GetCacheService(cacheServiceType);
        }

        protected IValidatorFactory ValidatorFactory { get; }
        protected ICacheServiceFactory CacheServiceFactory { get; }
        protected ISwitch<RequestAction, Func<TRequest, CancellationToken, Task<IAttempt>>> RequestValidation { get; }
        protected ISwitch<RequestAction, Func<TRequest, CancellationToken, Task<TResponse>>> RequestActions { get; }
    }
}
