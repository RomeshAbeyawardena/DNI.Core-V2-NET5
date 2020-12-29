using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Core.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddApiAuthentication<TApiAuthenticationHandler, TOptions>(this IServiceCollection services,
            string schemeName = "api-authentication",
            string schemeDisplayName = "API Authentication")
            where TApiAuthenticationHandler : AuthenticationHandler<TOptions>
            where TOptions : AuthenticationSchemeOptions, new()
        {
            return services
                .AddAuthentication(opt => opt.AddScheme<TApiAuthenticationHandler>(schemeName, schemeDisplayName));
        }

        public static IServiceCollection AddApiAuthorization<TAuthorizationPolicy>(this IServiceCollection services, 
            Action<AuthorizationOptions> configureAuthorization, params object[] policyArguments)
            where TAuthorizationPolicy : AuthorizationPolicy
        {
            return services.AddAuthorization(builder => { 
                builder.AddPolicy(nameof(TAuthorizationPolicy), 
                    Activator.CreateInstance(typeof(TAuthorizationPolicy), policyArguments) as TAuthorizationPolicy); 
                configureAuthorization(builder); 
            });
        }
    }
}
