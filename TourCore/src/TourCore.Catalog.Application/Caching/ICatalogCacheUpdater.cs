using System.Threading;
using System.Threading.Tasks;

namespace TourCore.Application.Abstractions.Caching
{
    public interface ICatalogCacheUpdater
    {
        Task CatalogChangedAsync(
            string key,
            CancellationToken cancellationToken);
    }
}