using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Caching;

namespace TourCore.Infrastructure.SqlServer.Caching.Legacy
{
    public class LegacyCatalogCacheUpdater : ICatalogCacheUpdater
    {
        private readonly ICatalogCacheInvalidator _cacheInvalidator;

        public LegacyCatalogCacheUpdater(
            ICatalogCacheInvalidator cacheInvalidator)
        {
            _cacheInvalidator = cacheInvalidator;
        }

        public Task CatalogChangedAsync(
            string key,
            CancellationToken cancellationToken)
        {
            return _cacheInvalidator.InvalidateAsync(
                key,
                cancellationToken);
        }
    }
}