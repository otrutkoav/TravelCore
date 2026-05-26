namespace TourCore.Application.Avia.Airlines.DTOs
{
    public class AirlineListFilter
    {
        /// <summary>
        /// Поиск по коду, названию, английскому названию и ICAO-коду авиакомпании.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Поиск по ICAO-коду авиакомпании.
        /// </summary>
        public string IcaoCode { get; set; }
    }
}