namespace TourCore.Application.Common.Queries
{
    /// <summary>
    /// Базовая модель запроса списка с пагинацией и сортировкой.
    /// </summary>
    public abstract class PagedQuery : IPagedQuery, ISortedQuery
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 20;
        public const int MaxPageSize = 100;

        private int _page = DefaultPage;
        private int _pageSize = DefaultPageSize;

        /// <summary>
        /// Номер страницы. Минимальное значение: 1. Если не передан или меньше 1, используется значение 1.
        /// </summary>
        public int Page
        {
            get { return _page; }
            set { _page = value <= 0 ? DefaultPage : value; }
        }

        /// <summary>
        /// Размер страницы. По умолчанию: 20. Максимальное значение: 100.
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value <= 0)
                {
                    _pageSize = DefaultPageSize;
                    return;
                }

                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }

        /// <summary>
        /// Поле сортировки. Допустимые значения зависят от конкретного справочника.
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Направление сортировки: 0 — по возрастанию, 1 — по убыванию.
        /// </summary>
        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}