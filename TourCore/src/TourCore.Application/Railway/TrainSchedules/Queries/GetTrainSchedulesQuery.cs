using TourCore.Application.Railway.TrainSchedules.DTOs;

namespace TourCore.Application.Railway.TrainSchedules.Queries
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