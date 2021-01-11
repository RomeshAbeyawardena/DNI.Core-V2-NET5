using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}
