using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SeaPizza.Infrastructure.Persistence.Context;

namespace SeaPizza.Infrastructure.Identity;

internal static class Startup
{
    internal static IServiceCollection AddIdentity(this IServiceCollection services) =>
        services
            .AddIdentity<SeaPizzaUser, SeaPizzaRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<SeaPizzaDbContext>()
            .AddDefaultTokenProviders()
            .Services;
}
