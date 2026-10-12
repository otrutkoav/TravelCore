using System;
using System.Linq.Expressions;
using TourCore.Contracts.Finance.RealCourses;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.Finance.RealCourses.Queries
{
    internal static class RealCourseProjections
    {
        public static readonly Expression<Func<RealCourse, RealCourseListItemDto>> ListItem =
            x => new RealCourseListItemDto
            {
                Id = x.Id,
                FromRateCode = x.FromRateCode,
                ToRateCode = x.ToRateCode,
                Course = x.Course,
                CentralBankCourse = x.CentralBankCourse,
                DateBeg = x.DateBeg,
                DateEnd = x.DateEnd
            };
    }
}