namespace TourCore.Application.RailwayTransfers.Commands
{
    public class CreateRailwayTransferCommand
    {
        public string Name { get; set; }

        public int CountryFromId { get; set; }
        public int CityFromId { get; set; }

        public int CountryToId { get; set; }
        public int CityToId { get; set; }
    }
}