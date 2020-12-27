using DNI.Core.Shared;
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
            using var cancellationTokenRegistration = new CancellationTokenRegistration();

            if(Request.Headers.TryGetValue("x-api-key", out var apiKey) 
                && Guid.TryParse(apiKey, out var apiKeyGuid))
            {
                var credential = await GetCredential(apiKeyGuid, cancellationTokenRegistration.Token);
                
                if(credential == null || (await IsCredentialValid(credential, cancellationTokenRegistration.Token)) == false)
                {
                    return AuthenticateResult.Fail("Unable to authorise API key: Credential not found");
                }

                if(Request.Headers.TryGetValue("auth-token", out var authorization) 
                    && credential.PassPhrase.Equals(authorization))
                {
                    if(!credential.IsMaster)
                    { 
                        credential.LastAccessed = Clock.UtcNow;
                        await SaveCredential(credential, cancellationTokenRegistration.Token);
                    }

                    var claimsIdentity = new ClaimsIdentity(Scheme.Name);

                    var claims = await GetCredentialClaims(credential, cancellationTokenRegistration.Token) ;

                    foreach (var claim in claims)
                    {
                        claimsIdentity.AddClaim(new Claim(claim.Key, claim.Value));
                    }

                    await OnAuthenticationSuccess(credential, cancellationTokenRegistration.Token);

                    return AuthenticateResult.Success(
                        new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name));
                }

                await OnAuthenticationFailure(credential, cancellationTokenRegistration.Token);

                return AuthenticateResult.Fail("Unable to authorise API key: Authentication not valid");
            }

            return AuthenticateResult.Fail("Unable to find API key header");
        }

        protected abstract Task OnAuthenticationSuccess(TCredential credential, CancellationToken cancellationToken);
        protected abstract Task OnAuthenticationFailure(TCredential credential, CancellationToken cancellationToken);
        protected abstract Task<bool> IsCredentialValid(TCredential credential, CancellationToken cancellationToken);
        protected abstract Task<IDictionary<string, string>> GetCredentialClaims(TCredential credential, CancellationToken cancellationToken);
        protected abstract Task SaveCredential(TCredential credential, CancellationToken cancellationToken);
        protected abstract Task<TCredential> GetCredential(Guid apiKey, CancellationToken cancellationToken);
    }
}
