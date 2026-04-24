using TourCore.Application.VehiclePlans.DTOs;

namespace TourCore.Application.VehiclePlans.Queries
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