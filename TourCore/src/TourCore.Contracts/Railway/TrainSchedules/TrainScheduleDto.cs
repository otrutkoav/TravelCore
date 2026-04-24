using System;

namespace TourCore.Contracts.Railway.TrainSchedules
{
    public class TrainScheduleDto
    {
        public int Id { get; set; }

        public int RailwayTransferId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime? TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public string DaysOfWeek { get; set; }

        public short? DaysOnRoad { get; set; }

        public string Remark { get; set; }
    }
}