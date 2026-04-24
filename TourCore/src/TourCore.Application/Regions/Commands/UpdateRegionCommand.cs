namespace TourCore.Application.Regions.Commands
{
    public class UpdateRegionCommand
    {
        public int Id { get; set; }
        public int CountryId { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public int SortOrder { get; set; }
    }
}