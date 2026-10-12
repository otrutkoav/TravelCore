using System.Collections.Generic;

namespace TourCore.Application.Common.Models
{
    public class ListResult<T>
    {
        public ListResult()
        {
            Items = new List<T>();
        }

        public IReadOnlyCollection<T> Items { get; set; }
        public int TotalCount { get; set; }

        public static ListResult<T> Create(IReadOnlyCollection<T> items)
        {
            return new ListResult<T>
            {
                Items = items ?? new List<T>(),
                TotalCount = items == null ? 0 : items.Count
            };
        }
    }
}