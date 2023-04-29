using SeaPizza.Application.Common.Interfaces;

namespace SeaPizza.Application.Common.Caching;

public interface ICacheKeyService : IScopedService
{
    public string GetCacheKey(string name, object id);
}
