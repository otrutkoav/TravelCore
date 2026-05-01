namespace TourCore.Application.Avia.Airports.DTOs
{
    public class AirportListFilter
    {
        public string Search { get; set; }
        public int? CityId { get; set; }
        public string IcaoCode { get; set; }
    }
}