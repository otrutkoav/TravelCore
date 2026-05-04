namespace TourCore.Application.Railway.RailwayTransfers.DTOs
{
    public class RailwayTransferListFilter
    {
        public string Search { get; set; }

        public int? CountryFromId { get; set; }
        public int? CityFromId { get; set; }

        public int? CountryToId { get; set; }
        public int? CityToId { get; set; }
    }
}