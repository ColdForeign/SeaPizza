using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeaPizza.Application.Common.Interfaces;
using SeaPizza.Infrastructure.Auth.Jwt;
using SeaPizza.Infrastructure.Auth.Permissions;
using SeaPizza.Infrastructure.Identity;

namespace SeaPizza.Infrastructure.Auth;

internal static class Startup
{
    internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        // Must add identity before adding auth!
        return services
            .AddCurrentUser()
            .AddPermissions()
            .AddIdentity()
            .AddJwtAuth();
    }

    internal static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app) =>
      app.UseMiddleware<CurrentUserMiddleware>();

    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());

    private static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
}
