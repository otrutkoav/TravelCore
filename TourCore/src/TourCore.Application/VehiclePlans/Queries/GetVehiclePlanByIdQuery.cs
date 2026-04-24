namespace TourCore.Application.VehiclePlans.Queries
{
    public class GetVehiclePlanByIdQuery
    {
        public GetVehiclePlanByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}