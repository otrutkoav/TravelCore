namespace TourCore.Contracts.Geography.Countries
{
    public class CountryListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }

        public string Code { get; set; }

        public string IsoCode2 { get; set; }

        public string IsoCode3 { get; set; }

        public string DigitalCode { get; set; }

        public string CitizenshipName { get; set; }

        public string CitizenshipNameEn { get; set; }

        public int SortOrder { get; set; }
    }
}