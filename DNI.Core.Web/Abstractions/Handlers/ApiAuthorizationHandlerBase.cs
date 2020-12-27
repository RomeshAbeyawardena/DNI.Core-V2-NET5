using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Web.Abstractions.Handlers
{
    public abstract class ApiAuthorizationHandlerBase<TApiHasAccessRequirement> : AuthorizationHandler<TApiHasAccessRequirement>
        where TApiHasAccessRequirement : IAuthorizationRequirement
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TApiHasAccessRequirement requirement)
        {
            using var cancellationTokenRegistration = new CancellationTokenRegistration();

            var httpContext = context.Resource as HttpContext;

            if(await HandleRequirement(requirement, httpContext, httpContext.User, httpContext.User?.Claims, cancellationTokenRegistration.Token))
            { 
                context.Succeed(requirement);
            }
        }

        protected abstract Task<bool> HandleRequirement(TApiHasAccessRequirement requirement, 
            HttpContext httpContext, 
            ClaimsPrincipal user, 
            IEnumerable<Claim> claims, 
            CancellationToken cancellationToken);
    }
}
