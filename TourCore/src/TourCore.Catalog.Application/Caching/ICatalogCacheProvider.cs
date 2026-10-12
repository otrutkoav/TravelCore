using System;
using System.Threading;
using System.Threading.Tasks;

namespace TourCore.Application.Abstractions.Caching
{
    public interface ICatalogCacheProvider
    {
        Task<T> GetOrCreateAsync<T>(
            string key,
            Func<Task<T>> factory,
            CancellationToken cancellationToken);
    }
}