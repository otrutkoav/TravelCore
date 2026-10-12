using System.Threading;
using System.Threading.Tasks;

namespace TourCore.Application.Abstractions.Caching
{
    public interface ICatalogCacheInvalidator
    {
        Task InvalidateAsync(
            string key,
            CancellationToken cancellationToken);
    }
}