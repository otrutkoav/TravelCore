using System;

namespace TourCore.Application.Avia.CharterSeasons.DTOs
{
    public class CharterSeasonListFilter
    {
        /// <summary>
        /// Фильтр по идентификатору базового чартерного рейса.
        /// </summary>
        public int? CharterId { get; set; }

        /// <summary>
        /// Фильтр по дате начала сезона.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Фильтр по дате окончания сезона.
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}