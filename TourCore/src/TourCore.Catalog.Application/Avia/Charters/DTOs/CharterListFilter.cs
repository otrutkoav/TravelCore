namespace TourCore.Application.Avia.Charters.DTOs
{
    public class CharterListFilter
    {
        /// <summary>
        /// Фильтр по идентификатору города вылета.
        /// </summary>
        public int? DepartureCityId { get; set; }

        /// <summary>
        /// Фильтр по идентификатору города прилета.
        /// </summary>
        public int? ArrivalCityId { get; set; }

        /// <summary>
        /// Поиск по коду аэропорта вылета.
        /// </summary>
        public string DepartureAirportCode { get; set; }

        /// <summary>
        /// Поиск по коду аэропорта прилета.
        /// </summary>
        public string ArrivalAirportCode { get; set; }

        /// <summary>
        /// Поиск по коду авиакомпании.
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Поиск по номеру рейса.
        /// </summary>
        public string FlightNumber { get; set; }
    }
}