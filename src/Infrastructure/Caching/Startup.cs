using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeaPizza.Application.Common.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Caching;

internal static class Startup
{
    internal static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration config)
    {
        return services.AddDistributedMemoryCache()
            .AddTransient<ICacheService, DistributedCacheService>()
            .AddTransient<ICacheService, LocalCacheService>()
            .AddMemoryCache();
    }
}
