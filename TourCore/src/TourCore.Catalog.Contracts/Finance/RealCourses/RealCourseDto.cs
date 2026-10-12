using System;

namespace TourCore.Contracts.Finance.RealCourses
{
    public class RealCourseDto
    {
        public int Id { get; set; }

        public string FromRateCode { get; set; }

        public string ToRateCode { get; set; }

        public decimal? Course { get; set; }

        public decimal? CentralBankCourse { get; set; }

        public DateTime? DateBeg { get; set; }

        public DateTime? DateEnd { get; set; }
    }
}