namespace TourCore.Application.Cities.DTOs
{
    public class CityListFilter
    {
        public string Search { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public bool? IsDeparturePoint { get; set; }
    }
}