using System;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Application.BusSchedules.DTOs
{
    public class BusScheduleListItemDto
    {
        public int Id { get; set; }

        public int BusTransferId { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string DaysOfWeek { get; set; }

        public short? DaysOnRoad { get; set; }
    }
}