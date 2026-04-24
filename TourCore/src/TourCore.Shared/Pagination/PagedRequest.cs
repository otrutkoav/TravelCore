using System;

namespace TourCore.Shared.Pagination
{
    public class PagedRequest
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 500;

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string SortBy { get; private set; }

        public SortDirection SortDirection { get; private set; }

        public PagedRequest()
        {
            PageNumber = DefaultPageNumber;
            PageSize = DefaultPageSize;
            SortDirection = SortDirection.Asc;
        }

        public PagedRequest(
            int pageNumber,
            int pageSize,
            string sortBy = null,
            SortDirection sortDirection = SortDirection.Asc)
        {
            PageNumber = pageNumber <= 0 ? DefaultPageNumber : pageNumber;
            PageSize = NormalizePageSize(pageSize);
            SortBy = sortBy;
            SortDirection = sortDirection;
        }

        private static int NormalizePageSize(int pageSize)
        {
            if (pageSize <= 0)
                return DefaultPageSize;

            if (pageSize > MaxPageSize)
                return MaxPageSize;

            return pageSize;
        }

        public int Skip
        {
            get { return (PageNumber - 1) * PageSize; }
        }
    }
}