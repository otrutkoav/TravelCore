using System;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Application.BusSchedules.DTOs
{
    public class BusScheduleDto
    {
        public int Id { get; set; }

        public int BusTransferId { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }

        public string DaysOfWeek { get; set; }

        public short? DaysOnRoad { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}