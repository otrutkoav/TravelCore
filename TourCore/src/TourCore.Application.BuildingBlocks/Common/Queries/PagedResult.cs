using System;
using System.Collections.Generic;

namespace TourCore.Application.Common.Queries
{
    public sealed class PagedResult<T>
    {
        public PagedResult(
            IReadOnlyCollection<T> items,
            int page,
            int pageSize,
            int totalCount)
        {
            Items = items ?? Array.Empty<T>();
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public IReadOnlyCollection<T> Items { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalCount { get; }
    }
}