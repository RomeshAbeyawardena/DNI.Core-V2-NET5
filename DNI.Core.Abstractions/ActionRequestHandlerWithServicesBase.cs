using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Meta;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Exceptions;
using DNI.Core.Shared.Options;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class ActionRequestHandlerWithServicesBase<TRequest, TResponse> 
        : ActionRequestHandlerBaseWithExceptionResourceFactory<TRequest, TResponse>
        where TRequest : IActionRequest<TResponse>
    {
        protected ActionRequestHandlerBaseWithServicesBase(ICacheServiceFactory cacheServiceFactory,
            IValidatorFactory validatorFactory,
            IExceptionResourceFactory exceptionResourceFactory,
            IETagService eTagService, IOptions<ETagServiceOptions> options)
            : base(cacheServiceFactory, validatorFactory, exceptionResourceFactory)
        {
            ETagService = eTagService;
            ETagServiceOptions = options;
        }

        protected bool ValidateETags<T>(T source, T model)
            where T : IETag
        {
            return ETagService.Validate(source, model, ETagServiceOptions.Value);
        }

        protected bool ValidateETags<T>(T source, string eTag)
            where T : IETag
        {
             return ETagService.Validate(source, eTag);
        }

        protected void EnsureETagsAreValid<T>(T source, T model)
            where T : IETag
        {
            if(!ValidateETags(source, model.ETag))
            {
                throw ExceptionResourceFactory
                    .GetException<T, ETagException>(false);
            }
        }

        protected void CalculateETag<T>(T model)
            where T : IETag
        {
            model.ETag = ETagService.Generate(model, ETagServiceOptions.Value);
        }

        protected IOptions<ETagServiceOptions> ETagServiceOptions { get; }
        protected IETagService ETagService { get; }
    }
}
