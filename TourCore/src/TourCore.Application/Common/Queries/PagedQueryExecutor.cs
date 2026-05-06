using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;

namespace TourCore.Application.Common.Queries
{
    public sealed class PagedQueryExecutor
    {
        private readonly IAsyncQueryExecutor _queryExecutor;

        public PagedQueryExecutor(IAsyncQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        public async Task<PagedResult<T>> ExecuteAsync<T>(
            IQueryable<T> query,
            IPagedQuery paging,
            CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (paging == null)
            {
                throw new ArgumentNullException(nameof(paging));
            }

            var totalCount = await _queryExecutor.CountAsync(query, cancellationToken);

            var items = await _queryExecutor.ToArrayAsync(
                query.ApplyPaging(paging),
                cancellationToken);

            return new PagedResult<T>(
                items,
                paging.Page,
                paging.PageSize,
                totalCount);
        }
    }
}