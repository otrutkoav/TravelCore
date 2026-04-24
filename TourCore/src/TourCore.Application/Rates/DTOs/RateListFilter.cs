namespace TourCore.Application.Rates.DTOs
{
    public class RateListFilter
    {
        public string Search { get; set; }
        public bool? IsMain { get; set; }
        public bool? IsNational { get; set; }
        public bool? ShowInSearch { get; set; }
    }
}