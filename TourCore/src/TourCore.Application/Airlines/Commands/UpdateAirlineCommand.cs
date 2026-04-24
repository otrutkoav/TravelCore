namespace TourCore.Application.Airlines.Commands
{
    public class UpdateAirlineCommand
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string IcaoCode { get; set; }
    }
}