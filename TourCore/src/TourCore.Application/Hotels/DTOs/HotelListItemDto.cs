namespace TourCore.Application.Hotels.DTOs
{
    public class HotelListItemDto
    {
        public int Id { get; set; }

        public int CountryId { get; set; }
        public int CityId { get; set; }

        public string Name { get; set; }
        public string Stars { get; set; }
        public string Code { get; set; }

        public int SortOrder { get; set; }
        public int? Rank { get; set; }
    }
}