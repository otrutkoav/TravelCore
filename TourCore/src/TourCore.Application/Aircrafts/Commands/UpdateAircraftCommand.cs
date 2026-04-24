namespace TourCore.Application.Aircrafts.Commands
{
    public class UpdateAircraftCommand
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
    }
}