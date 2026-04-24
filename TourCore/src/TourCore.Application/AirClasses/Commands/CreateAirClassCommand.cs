namespace TourCore.Application.AirClasses.Commands
{
    public class CreateAirClassCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Group { get; set; }
        public int SortOrder { get; set; }
    }
}