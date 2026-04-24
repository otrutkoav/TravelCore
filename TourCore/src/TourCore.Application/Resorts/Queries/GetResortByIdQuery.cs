namespace TourCore.Application.Resorts.Queries
{
    public class GetResortByIdQuery
    {
        public GetResortByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}