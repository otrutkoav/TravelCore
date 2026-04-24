namespace TourCore.Application.Countries.Queries
{
    public class GetCountryByIdQuery
    {
        public GetCountryByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}