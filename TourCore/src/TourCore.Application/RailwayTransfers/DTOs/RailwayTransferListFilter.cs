namespace TourCore.Application.RailwayTransfers.DTOs
{
    public class RailwayTransferListFilter
    {
        public int? CountryFromId { get; set; }
        public int? CityFromId { get; set; }

        public int? CountryToId { get; set; }
        public int? CityToId { get; set; }
    }
}