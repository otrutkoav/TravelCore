namespace TourCore.Search.Application.Models
{
    public sealed class SearchHotelRow
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Stars { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ResortId { get; set; }
        public string ResortName { get; set; }
        public string Address { get; set; }
        public int? Rank { get; set; }
        public int SortOrder { get; set; }
    }
}
