using TourCore.Application.Abstractions.Caching;

namespace TourCore.Admin.Api.Infrastructure;

internal sealed class NoCacheCatalogServices :
    ICatalogCacheProvider,
    ICatalogCacheUpdater,
    ICatalogCacheInvalidator
{
    public Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return factory();
    }

    public Task CatalogChangedAsync(string key, CancellationToken cancellationToken) => Task.CompletedTask;
    public Task InvalidateAsync(string key, CancellationToken cancellationToken) => Task.CompletedTask;
}
