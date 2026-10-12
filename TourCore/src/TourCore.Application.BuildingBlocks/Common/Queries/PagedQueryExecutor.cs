using System;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Data;

namespace TourCore.Application.Common.Queries
{
    public sealed class PagedQueryExecutor
    {
        private readonly IQuerySpecificationExecutor _queryExecutor;

        public PagedQueryExecutor(IQuerySpecificationExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        public async Task<PagedResult<TResult>> ExecuteAsync<TEntity, TResult>(
            ProjectedQuerySpecification<TEntity, TResult> query,
            IPagedQuery paging,
            CancellationToken cancellationToken)
            where TEntity : class
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
                query,
                (paging.Page - 1) * paging.PageSize,
                paging.PageSize,
                cancellationToken);

            return new PagedResult<TResult>(
                items,
                paging.Page,
                paging.PageSize,
                totalCount);
        }
    }
}
