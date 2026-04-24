namespace TourCore.Application.ServiceTypes.DTOs
{
    public class ServiceTypeListFilter
    {
        public string Search { get; set; }
        public bool? IsRoute { get; set; }
        public bool? IsCity { get; set; }
        public bool? HasPrimaryParameter { get; set; }
        public bool? HasSecondaryParameter { get; set; }
        public bool? IsQuoted { get; set; }
        public bool? IsIndividual { get; set; }
    }
}