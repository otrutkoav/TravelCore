namespace TourCore.Application.Geography.Resorts.Commands
{
    public class UpdateResortCommand
    {
        public int Id { get; set; }
        public int CountryId { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
    }
}