using System;
using System.Collections.Generic;

namespace TourCore.Contracts.Common
{
    public sealed class PagedResponseDto<T>
    {
        public PagedResponseDto(
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