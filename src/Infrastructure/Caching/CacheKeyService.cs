using SeaPizza.Application.Common.Caching;
using System;

namespace SeaPizza.Infrastructure.Caching;

public class CacheKeyService : ICacheKeyService
{
    public string GetCacheKey(string name, object id)
    {

        return $"SeaPizza-{name}-{id}";
    }
}
