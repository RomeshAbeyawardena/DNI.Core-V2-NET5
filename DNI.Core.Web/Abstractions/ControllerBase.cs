﻿using AutoMapper;
using DNI.Core.Abstractions.Extensions;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
                (attempt) => BadRequest(new Shared.Dto.Exception(attempt.Exception)));
        }

        protected IActionResult ValidateAttemptedResponse<T>(IAttemptedResponse<T> attempt)
        {
            return attempt.AttemptedResponseResult<T, IActionResult>(
                (result, type) => Ok(result), 
                (attempt) => BadRequest(new Shared.Dto.Exception(attempt.Exception)));
        }

        protected IActionResult ValidateAttemptedResponse<T, TResult>(IAttemptedResponse<T> attempt)
        {
            return attempt.AttemptedResponseResult<T, IActionResult>(
                (result, type) => Ok(type == RequestQueryType.Multiple 
                    ? Mapper.Map<IEnumerable<TResult>>(result) 
                    : Mapper.Map<TResult>(result)),
                (attempt) => BadRequest(new Shared.Dto.Exception(attempt.Exception)));
        }


        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }
    }
}