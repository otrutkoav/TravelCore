namespace TourCore.Contracts.Transportation.Transports
{
    public class TransportListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }

        public short? SeatsCount { get; set; }

        public int ShowOrder { get; set; }
    }
}