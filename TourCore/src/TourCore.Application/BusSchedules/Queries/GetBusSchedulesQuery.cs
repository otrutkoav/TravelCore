using TourCore.Application.BusSchedules.DTOs;

namespace TourCore.Application.BusSchedules.Queries
{
    public class GetBusSchedulesQuery
    {
        public GetBusSchedulesQuery()
        {
            Filter = new BusScheduleListFilter();
        }

        public BusScheduleListFilter Filter { get; set; }
    }
}