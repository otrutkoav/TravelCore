using TourCore.Application.Finance.RealCourses.DTOs;

namespace TourCore.Application.Finance.RealCourses.Queries
{
    public class GetRealCoursesQuery
    {
        public GetRealCoursesQuery()
        {
            Filter = new RealCourseListFilter();
        }

        public RealCourseListFilter Filter { get; set; }
    }
}