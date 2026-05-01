using TourCore.Application.Seating.VehiclePlans.DTOs;

namespace TourCore.Application.Seating.VehiclePlans.Queries
{
    public class GetVehiclePlansQuery
    {
        public GetVehiclePlansQuery()
        {
            Filter = new VehiclePlanListFilter();
        }

        public VehiclePlanListFilter Filter { get; set; }
    }
}