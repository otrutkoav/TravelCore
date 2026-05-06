namespace TourCore.Application.Geography.Cities.DTOs
{
    /// <summary>
    /// Фильтр списка городов.
    /// </summary>
    public class CityListFilter
    {
        /// <summary>
        /// Поиск по названию, английскому названию, коду города и IATA-коду.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Идентификатор страны.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Идентификатор региона.
        /// </summary>
        public int? RegionId { get; set; }

        /// <summary>
        /// Признак города вылета.
        /// </summary>
        public bool? IsDeparturePoint { get; set; }
    }
}