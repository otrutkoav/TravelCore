namespace TourCore.Application.Rates.Commands
{
    public class CreateRateCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }

        public bool IsMain { get; set; }
        public bool IsNational { get; set; }
        public bool ShowInSearch { get; set; }

        public byte[] Symbol { get; set; }
    }
}