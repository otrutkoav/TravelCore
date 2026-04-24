namespace TourCore.Application.BusTransfers.DTOs
{
    public class BusTransferListFilter
    {
        public string Search { get; set; }
        public int? CountryFromId { get; set; }
        public int? CityFromId { get; set; }
        public int? CountryToId { get; set; }
        public int? CityToId { get; set; }
    }
}