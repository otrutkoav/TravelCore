using System.Collections.Generic;

namespace TourCore.Search.Contracts
{
    /// <summary>Страница результатов с общим количеством найденных элементов.</summary>
    /// <typeparam name="T">Тип элемента страницы.</typeparam>
    public sealed class SearchPagedResult<T>
    {
        public SearchPagedResult(IReadOnlyCollection<T> items, int page, int pageSize, int totalCount)
        {
            Items = items ?? new T[0];
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        /// <summary>Элементы текущей страницы.</summary>
        public IReadOnlyCollection<T> Items { get; }
        /// <summary>Номер текущей страницы.</summary>
        public int Page { get; }
        /// <summary>Запрошенный размер страницы.</summary>
        public int PageSize { get; }
        /// <summary>Общее количество кандидатов до пагинации.</summary>
        public int TotalCount { get; }
    }
}
