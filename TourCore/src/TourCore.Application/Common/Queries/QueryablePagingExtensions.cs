using System;
using System.Linq;

namespace TourCore.Application.Common.Queries
{
    public static class QueryablePagingExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(
            this IQueryable<T> query,
            IPagedQuery paging)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (paging == null)
            {
                throw new ArgumentNullException(nameof(paging));
            }

            var skip = (paging.Page - 1) * paging.PageSize;

            return query
                .Skip(skip)
                .Take(paging.PageSize);
        }
    }
}