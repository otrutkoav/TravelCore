namespace TourCore.Contracts.Geography.Cities
{
    public class CityListItemDto
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        public int? RegionId { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }

        public string Code { get; set; }

        public int SortOrder { get; set; }

        public bool IsDeparturePoint { get; set; }

        public string TimeZone { get; set; }

        public string IataCode { get; set; }
    }
}