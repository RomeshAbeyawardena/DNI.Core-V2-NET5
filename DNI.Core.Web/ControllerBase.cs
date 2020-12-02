using AutoMapper;
using DNI.Core.Shared.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DNI.Core.Web
{
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected ControllerBase(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        public IMediator Mediator { get; }

        public IMapper Mapper { get; }

        public IActionResult AttemptedResult<T>(IAttempt<T> attempt, bool useCreatedResponse = false, string key = default)
        {
            if (attempt.Successful)
            {
                if(useCreatedResponse)
                { 
                    return Created(GetCurrentUrl(),Request.Scheme);
                }

                return Ok(attempt.Result);
            }

            if(key == default)
            {
                key = string.Empty;
            }

            ModelState.AddModelError(key, attempt.Exception.Message);

            return BadRequest(ModelState);
        }

        protected Uri GetCurrentUrl()
        {
            var request = HttpContext.Request;
            return new Uri($"{request.Scheme}://{request.Host}"); 
        }
    }
}
