namespace TourCore.Search.Contracts
{
    /// <summary>Пара транспортных сегментов одного вида туда и обратно.</summary>
    public sealed class RoundTripTransport
    {
        /// <summary>Вид транспорта обоих сегментов.</summary>
        public SearchTransportKind Kind { get; set; }
        /// <summary>Сегмент из города отправления в город назначения.</summary>
        public RouteSegment Outbound { get; set; }
        /// <summary>Сегмент из города назначения обратно.</summary>
        public RouteSegment Inbound { get; set; }
    }
}
