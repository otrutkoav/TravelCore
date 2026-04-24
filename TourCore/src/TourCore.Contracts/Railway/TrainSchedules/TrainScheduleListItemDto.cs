namespace TourCore.Contracts.Railway.TrainSchedules
{
    public class TrainScheduleListItemDto
    {
        public int Id { get; set; }

        public int RailwayTransferId { get; set; }

        public string Remark { get; set; }
    }
}