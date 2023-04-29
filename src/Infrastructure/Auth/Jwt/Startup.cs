using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SeaPizza.Infrastructure.Auth.Jwt;

internal static class Startup
{
    internal static IServiceCollection AddJwtAuth(this IServiceCollection services)
    {
        return services.AddOptions<JwtSettings>()
            .BindConfiguration($"SecuritySettings:{nameof(JwtSettings)}")
            .ValidateDataAnnotations()
            .ValidateOnStart().Services;
    }
}
