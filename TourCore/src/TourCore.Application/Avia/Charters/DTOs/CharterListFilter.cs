namespace TourCore.Application.Avia.Charters.DTOs
{
    public class CharterListFilter
    {
        public int? DepartureCityId { get; set; }
        public int? ArrivalCityId { get; set; }

        public string DepartureAirportCode { get; set; }
        public string ArrivalAirportCode { get; set; }

        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
    }
}