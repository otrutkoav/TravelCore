namespace TourCore.Application.Hotels.Hotels.DTOs
{
    public class HotelListFilter
    {
        /// <summary>
        /// Поиск по коду, названию и английскому названию отеля.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Фильтр по идентификатору страны.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Фильтр по идентификатору города.
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Фильтр по идентификатору курорта.
        /// </summary>
        public int? ResortId { get; set; }

        /// <summary>
        /// Фильтр по идентификатору категории отеля.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Фильтр по признаку круизного отеля.
        /// </summary>
        public bool? IsCruise { get; set; }
    }
}