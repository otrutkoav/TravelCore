namespace TourCore.Application.VehiclePlans.DTOs
{
    public class VehiclePlanListFilter
    {
        public int? TransportId { get; set; }
        public bool? IsAircraft { get; set; }
        public string Search { get; set; }
    }
}