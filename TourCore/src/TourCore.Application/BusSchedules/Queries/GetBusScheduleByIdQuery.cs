namespace TourCore.Application.BusSchedules.Queries
{
    public class GetBusScheduleByIdQuery
    {
        public GetBusScheduleByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}