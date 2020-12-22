using AutoMapper;
using DNI.Core.Abstractions.Extensions;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Web.Abstractions
{
    [Route("[controller]")]
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    { 
        protected ControllerBase(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        protected IActionResult ValidateAttempt<T>(IAttempt<T> attempt)
        {
            return attempt.AttemptedResult<T, IActionResult>(
                result => Ok(result), 
                (attempt) => BadRequest(attempt.Exception));
        }

        protected IActionResult ValidateAttemptedResponse<T>(IAttemptedResponse<T> attempt)
        {
            return attempt.AttemptedResponseResult<T, IActionResult>(
                result => Ok(result), 
                (attempt) => BadRequest(attempt.Exception));
        }

        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }
    }
}
