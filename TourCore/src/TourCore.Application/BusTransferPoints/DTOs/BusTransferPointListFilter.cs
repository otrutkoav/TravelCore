namespace TourCore.Application.BusTransferPoints.DTOs
{
    public class BusTransferPointListFilter
    {
        public int? BusTransferId { get; set; }
        public int? CountryFromId { get; set; }
        public int? CityFromId { get; set; }
        public int? CountryToId { get; set; }
        public int? CityToId { get; set; }
    }
}