using System;

namespace TourCore.Application.Avia.CharterSeasons.DTOs
{
    public class CharterSeasonListFilter
    {
        public int? CharterId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}