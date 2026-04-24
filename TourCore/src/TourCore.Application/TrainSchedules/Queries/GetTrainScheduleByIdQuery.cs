namespace TourCore.Application.TrainSchedules.Queries
{
    public class GetTrainScheduleByIdQuery
    {
        public GetTrainScheduleByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}