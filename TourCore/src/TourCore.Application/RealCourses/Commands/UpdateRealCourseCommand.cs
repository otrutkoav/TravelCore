using System;

namespace TourCore.Application.RealCourses.Commands
{
    public class UpdateRealCourseCommand
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