using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DNI.Core.Web.Abstractions.Handlers
{
    public abstract class ApiAuthorizationHandlerBase<TApiHasAccessRequirement> : AuthorizationHandler<TApiHasAccessRequirement>
        where TApiHasAccessRequirement : IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TApiHasAccessRequirement requirement)
        {
            var httpContext = context.Resource as HttpContext;

            if(HandleRequirement(requirement, httpContext, httpContext.User, httpContext.User?.Claims))
            { 
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        protected abstract bool HandleRequirement(TApiHasAccessRequirement requirement, 
            HttpContext httpContext, 
            ClaimsPrincipal user, 
            IEnumerable<Claim> claims);
    }
}
