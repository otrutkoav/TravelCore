using System;

namespace TourCore.Application.Finance.RealCourses.DTOs
{
    /// <summary>
    /// Фильтр списка курсов валют.
    /// </summary>
    public class RealCourseListFilter
    {
        /// <summary>
        /// Код исходной валюты.
        /// </summary>
        public string FromRateCode { get; set; }

        /// <summary>
        /// Код целевой валюты.
        /// </summary>
        public string ToRateCode { get; set; }

        /// <summary>
        /// Начало периода действия курса.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Конец периода действия курса.
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}