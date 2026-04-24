namespace TourCore.Application.Transfers.DTOs
{
    public class TransferListFilter
    {
        public string Search { get; set; }
        public int? CityId { get; set; }
        public int? DirectionId { get; set; }
        public bool? IsMain { get; set; }
    }
}