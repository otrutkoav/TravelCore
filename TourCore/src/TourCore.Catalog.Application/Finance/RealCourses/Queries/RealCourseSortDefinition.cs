using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.Finance.RealCourses.Queries
{
    internal static class RealCourseSortDefinition
    {
        public static readonly SortDefinition<RealCourse> Instance =
            new SortDefinition<RealCourse>(
                new SortExpression<RealCourse>[]
                {
                    new SortExpression<RealCourse, int>("id", x => x.Id),
                    new SortExpression<RealCourse, string>("fromRateCode", x => x.FromRateCode),
                    new SortExpression<RealCourse, string>("toRateCode", x => x.ToRateCode),
                    new SortExpression<RealCourse, decimal?>("course", x => x.Course),
                    new SortExpression<RealCourse, decimal?>("centralBankCourse", x => x.CentralBankCourse),
                    new SortExpression<RealCourse, DateTime?>("dateBeg", x => x.DateBeg),
                    new SortExpression<RealCourse, DateTime?>("dateEnd", x => x.DateEnd)
                },
                (Expression<Func<RealCourse, string>>)(x => x.FromRateCode));
    }
}