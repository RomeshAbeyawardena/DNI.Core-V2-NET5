using DNI.Core.Shared.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Web.Abstractions.Handlers
{
    public abstract class ApiAuthenticationHandlerBase<TCredential, TOptions> : AuthenticationHandler<TOptions>
        where TOptions : AuthenticationSchemeOptions, new()
        where TCredential : ICredential<Guid>
    {
        protected ApiAuthenticationHandlerBase(IOptionsMonitor<TOptions> options, ILoggerFactory logger, UrlEncoder encoder, Microsoft.AspNetCore.Authentication.ISystemClock clock) 
            : base(options, logger, encoder, clock)
        {
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(Request.Headers.TryGetValue("x-api-key", out var apiKey) 
                && Guid.TryParse(apiKey, out var apiKeyGuid))
            {
                var credential = await GetCredential(apiKeyGuid, CancellationToken.None);
                
                if(credential == null)
                {
                    return AuthenticateResult.Fail("Unable to authorise API key: Credential not found");
                }

                if(Request.Headers.TryGetValue("auth-token", out var authorization) 
                    && credential.PassPhrase.Equals(authorization))
                {
                    if(!credential.IsMaster)
                    { 
                        credential.LastAccessed = Clock.UtcNow;
                        await SaveCredential(credential, CancellationToken.None);
                    }

                    var claimsIdentity = new ClaimsIdentity(Scheme.Name);

                    var claims = await GetCredentialClaims(credential);

                    foreach (var claim in claims)
                    {
                        claimsIdentity.AddClaim(new Claim(claim.Key, claim.Value));
                    }

                    return AuthenticateResult.Success(
                        new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name));
                }

                return AuthenticateResult.Fail("Unable to authorise API key: Authentication not valid");
            }

            return AuthenticateResult.Fail("Unable to find API key header");
        }

        protected abstract Task<Dictionary<string, string>> GetCredentialClaims(TCredential credential);
        protected abstract Task SaveCredential(TCredential credential, CancellationToken cancellationToken);
        protected abstract Task<TCredential> GetCredential(Guid apiKey, CancellationToken cancellationToken);
    }
}
