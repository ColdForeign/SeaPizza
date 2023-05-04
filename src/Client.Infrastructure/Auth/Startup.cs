using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeaPizza.Client.Infrastructure.Auth.Jwt;

namespace SeaPizza.Client.Infrastructure.Auth;

internal static class Startup
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config) =>
       config[nameof(AuthProvider)] switch
       {
           // Jwt
           _ => services
               .AddScoped<AuthenticationStateProvider, JwtAuthenticationService>()
               .AddScoped(sp => (IAuthenticationService)sp.GetRequiredService<AuthenticationStateProvider>())
               .AddScoped(sp => (IAccessTokenProvider)sp.GetRequiredService<AuthenticationStateProvider>())
               .AddScoped<IAccessTokenProviderAccessor, AccessTokenProviderAccessor>()
               .AddScoped<JwtAuthenticationHeaderHandler>()
       };

    public static IHttpClientBuilder AddAuthenticationHandler(this IHttpClientBuilder builder, IConfiguration config) =>
        config[nameof(AuthProvider)] switch
        {
            // Jwt
            _ => builder.AddHttpMessageHandler<JwtAuthenticationHeaderHandler>()
        };

}
