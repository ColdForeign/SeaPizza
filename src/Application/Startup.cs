using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SeaPizza.Application;

public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services
            .AddValidatorsFromAssembly(assembly);
    }
}
