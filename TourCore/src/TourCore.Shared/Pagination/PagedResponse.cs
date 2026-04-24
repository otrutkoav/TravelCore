using System;
using System.Collections.Generic;
using System.Linq;

namespace TourCore.Shared.Pagination
{
    public class PagedResponse<T>
    {
        public IReadOnlyList<T> Items { get; private set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public int TotalPages
        {
            get
            {
                if (PageSize <= 0)
                    return 0;

                return (int)Math.Ceiling((double)TotalCount / PageSize);
            }
        }

        public bool HasPreviousPage
        {
            get { return PageNumber > 1; }
        }

        public bool HasNextPage
        {
            get { return PageNumber < TotalPages; }
        }

        public PagedResponse(
            IEnumerable<T> items,
            int pageNumber,
            int pageSize,
            int totalCount)
        {
            Items = items == null
                ? new List<T>()
                : items.ToList();

            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}