using System;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Application.TrainSchedules.Commands
{
    public class CreateTrainScheduleCommand
    {
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