using Microsoft.EntityFrameworkCore;
using TourCore.Application.Common.Data;

namespace TourCore.Infrastructure.Postgres.Common;

public sealed class EfCoreAsyncQueryExecutor : IAsyncQueryExecutor
{
    public Task<int> CountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) =>
        query.CountAsync(cancellationToken);

    public async Task<IReadOnlyCollection<T>> ToArrayAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) =>
        await query.ToArrayAsync(cancellationToken);
}
