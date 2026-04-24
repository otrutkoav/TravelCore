namespace TourCore.Application.Hotels.DTOs
{
    public class HotelListFilter
    {
        public string Search { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? ResortId { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsCruise { get; set; }
    }
}