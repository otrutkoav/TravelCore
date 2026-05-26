using System;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Caching;

namespace TourCore.Infrastructure.SqlServer.Caching.Legacy
{
    public class LegacyCatalogCacheProvider : ICatalogCacheProvider
    {
        private readonly ObjectCache _cache;

        public LegacyCatalogCacheProvider()
        {
            _cache = MemoryCache.Default;
        }

        public async Task<T> GetOrCreateAsync<T>(
            string key,
            Func<Task<T>> factory,
            CancellationToken cancellationToken)
        {
            var cached = _cache.Get(key);

            if (cached != null)
                return (T)cached;

            cancellationToken.ThrowIfCancellationRequested();

            var value = await factory();

            _cache.Set(
                key,
                value,
                new CacheItemPolicy
                {
                    Priority = CacheItemPriority.NotRemovable
                });

            return value;
        }
    }
}