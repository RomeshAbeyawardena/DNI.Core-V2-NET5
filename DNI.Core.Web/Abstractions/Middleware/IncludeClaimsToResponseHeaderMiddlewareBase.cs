using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DNI.Core.Web.Abstractions.Middleware
{
    public abstract class IncludeClaimsToResponseHeaderMiddlewareBase : MiddlewareBase
    {
        protected IncludeClaimsToResponseHeaderMiddlewareBase(RequestDelegate next) 
            : base(next)
        {
        }

        protected override async Task<bool> InvokeMiddleware(HttpContext context)
        {
            if(await IncludeClaimsToResponseHeaderCheck(context.RequestServices, context))
            { 
                foreach (var claim in context.User.Claims)
                {
                    context.Response.Headers.Add(claim.Type, claim.Value);
                }
            }
            return true;
        }

        protected abstract Task<bool> IncludeClaimsToResponseHeaderCheck(IServiceProvider serviceProvider, HttpContext context);
    }
}
