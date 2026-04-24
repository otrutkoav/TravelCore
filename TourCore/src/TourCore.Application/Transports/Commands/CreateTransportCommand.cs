namespace TourCore.Application.Transports.Commands
{
    public class CreateTransportCommand
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public short? SeatsCount { get; set; }
        public int ShowOrder { get; set; }
    }
}