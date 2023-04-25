using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeaPizza.Infrastructure.Identity;

namespace SeaPizza.Infrastructure.Auth;

internal static class Startup
{
    internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        // Must add identity before adding auth!
        return services.AddIdentity();
    }
}
