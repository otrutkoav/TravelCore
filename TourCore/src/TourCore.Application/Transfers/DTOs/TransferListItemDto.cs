namespace TourCore.Application.Transfers.DTOs
{
    public class TransferListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsMain { get; set; }

        public int? CityId { get; set; }
        public int? DirectionId { get; set; }

        public int ShowOrder { get; set; }
    }
}