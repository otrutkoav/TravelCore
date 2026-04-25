using System;

namespace TourCore.Contracts.Avia.CharterSeasons
{
    public class CharterSeasonListItemDto
    {
        public int Id { get; set; }

        public int CharterId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string DaysOfWeek { get; set; }

        public DateTime? TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public bool IsNextDayArrival { get; set; }

        public string Remark { get; set; }
    }
}