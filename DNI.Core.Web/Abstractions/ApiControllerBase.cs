using AutoMapper;
using DNI.Core.Shared.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Web.Abstractions
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }

    public class ApiControllerBase<TRequest, TResponse, TResult> : ApiControllerBase
        where TRequest : IRequest<TResponse>
        where TResponse : IAttemptedResponse<TResult>
    {
        protected ApiControllerBase(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        protected async Task<IActionResult> Process(TRequest request, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(request, cancellationToken);

            return ValidateAttemptedResponse(response);
        }
    }
}
