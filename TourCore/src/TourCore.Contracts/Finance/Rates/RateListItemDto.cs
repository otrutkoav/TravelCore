namespace TourCore.Contracts.Finance.Rates
{
    public class RateListItemDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string IsoCode { get; set; }

        public bool IsMain { get; set; }

        public bool IsNational { get; set; }

        public bool ShowInSearch { get; set; }
    }
}