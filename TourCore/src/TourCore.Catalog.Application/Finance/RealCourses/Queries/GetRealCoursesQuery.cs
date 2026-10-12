using TourCore.Application.Common.Queries;
using TourCore.Application.Finance.RealCourses.DTOs;

namespace TourCore.Application.Finance.RealCourses.Queries
{
    /// <summary>
    /// Запрос списка курсов валют.
    /// </summary>
    public class GetRealCoursesQuery : PagedQuery
    {
        public GetRealCoursesQuery()
        {
            Filter = new RealCourseListFilter();
        }

        /// <summary>
        /// Фильтр списка курсов валют.
        /// </summary>
        public RealCourseListFilter Filter { get; set; }
    }
}