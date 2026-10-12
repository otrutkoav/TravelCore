namespace TourCore.Application.Hotels.AccommodationTypes.DTOs
{
    public class AccommodationTypeListFilter
    {
        /// <summary>
        /// Поиск по коду, названию и английскому названию типа размещения.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Фильтр по признаку основного размещения.
        /// </summary>
        public bool? IsMain { get; set; }
    }
}