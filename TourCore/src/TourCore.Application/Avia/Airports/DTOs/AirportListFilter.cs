namespace TourCore.Application.Avia.Airports.DTOs
{
    public class AirportListFilter
    {
        /// <summary>
        /// Поиск по коду, названию, английскому названию, ICAO-коду и буквенной метке аэропорта.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Фильтр по идентификатору города.
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Поиск по ICAO-коду аэропорта.
        /// </summary>
        public string IcaoCode { get; set; }
    }
}