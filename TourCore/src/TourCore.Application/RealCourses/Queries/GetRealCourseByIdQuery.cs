namespace TourCore.Application.RealCourses.Queries
{
    public class GetRealCourseByIdQuery
    {
        public GetRealCourseByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}