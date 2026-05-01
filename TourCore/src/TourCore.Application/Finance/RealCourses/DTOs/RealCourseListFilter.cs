using System;

namespace TourCore.Application.Finance.RealCourses.DTOs
{
    public class RealCourseListFilter
    {
        public string FromRateCode { get; set; }

        public string ToRateCode { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}