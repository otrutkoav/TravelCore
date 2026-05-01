namespace TourCore.Application.Geography.Cities.Queries
{
    public class GetCityByIdQuery
    {
        public GetCityByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}