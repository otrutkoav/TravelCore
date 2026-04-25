namespace TourCore.Contracts.Railway.RailwayTransfers
{
    public class RailwayTransferListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryFromId { get; set; }
        public int CityFromId { get; set; }

        public int CountryToId { get; set; }
        public int CityToId { get; set; }
    }
}