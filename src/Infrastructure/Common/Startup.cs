using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Common;

internal static class Startup
{
    internal static IServiceCollection AddService(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime) =>
       lifetime switch
       {
           ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
           ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
           ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
           _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
       };

}
