using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Caching;

namespace TourCore.Infrastructure.SqlServer.Caching.Legacy
{
    public class LegacyCatalogCacheInvalidator : ICatalogCacheInvalidator
    {
        private readonly ObjectCache _cache;

        public LegacyCatalogCacheInvalidator()
        {
            _cache = MemoryCache.Default;
        }

        public Task InvalidateAsync(
            string key,
            CancellationToken cancellationToken)
        {
            _cache.Remove(key);

            return Task.CompletedTask;
        }
    }
}