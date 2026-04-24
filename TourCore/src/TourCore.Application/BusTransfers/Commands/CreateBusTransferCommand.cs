namespace TourCore.Application.BusTransfers.Commands
{
    public class CreateBusTransferCommand
    {
        public string Name { get; set; }

        public int CountryFromId { get; set; }
        public int CityFromId { get; set; }

        public int CountryToId { get; set; }
        public int CityToId { get; set; }
    }
}