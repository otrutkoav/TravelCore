using TourCore.Application.RealCourses.DTOs;

namespace TourCore.Application.RealCourses.Queries
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