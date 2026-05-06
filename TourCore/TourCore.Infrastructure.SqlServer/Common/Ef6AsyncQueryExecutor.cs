using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;

namespace TourCore.Infrastructure.SqlServer.Common
{
    public sealed class Ef6AsyncQueryExecutor : IAsyncQueryExecutor
    {
        public Task<int> CountAsync<T>(
            IQueryable<T> query,
            CancellationToken cancellationToken)
        {
            return query.CountAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<T>> ToArrayAsync<T>(
            IQueryable<T> query,
            CancellationToken cancellationToken)
        {
            return await query.ToArrayAsync(cancellationToken);
        }
    }
}