namespace TourCore.Application.Charters.DTOs
{
    public class CharterListFilter
    {
        public int? DepartureCityId { get; set; }
        public int? ArrivalCityId { get; set; }
        public string FlightNumber { get; set; }
        public string AirlineCode { get; set; }
    }
}