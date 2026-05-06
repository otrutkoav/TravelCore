using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TourCore.Application.Common.Data
{
    public interface IAsyncQueryExecutor
    {
        Task<int> CountAsync<T>(
            IQueryable<T> query,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<T>> ToArrayAsync<T>(
            IQueryable<T> query,
            CancellationToken cancellationToken);
    }
}