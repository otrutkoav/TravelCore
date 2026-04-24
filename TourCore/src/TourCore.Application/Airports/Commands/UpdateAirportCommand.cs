namespace TourCore.Application.Airports.Commands
{
    public class UpdateAirportCommand
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string IcaoCode { get; set; }
        public string LetterCode { get; set; }
    }
}