using System.Collections.Generic;

namespace TourCore.Application.Common.Models
{
    public class PagedResult<T>
    {
        public PagedResult()
        {
            Items = new List<T>();
        }

        public IReadOnlyCollection<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}