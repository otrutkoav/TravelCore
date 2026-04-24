namespace TourCore.Application.Cities.Queries
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