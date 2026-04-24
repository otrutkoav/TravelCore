using TourCore.Application.TrainSchedules.DTOs;

namespace TourCore.Application.TrainSchedules.Queries
{
    public class GetTrainSchedulesQuery
    {
        public GetTrainSchedulesQuery()
        {
            Filter = new TrainScheduleListFilter();
        }

        public TrainScheduleListFilter Filter { get; set; }
    }
}