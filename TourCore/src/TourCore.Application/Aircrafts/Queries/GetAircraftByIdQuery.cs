namespace TourCore.Application.Aircrafts.Queries
{
    public class GetAircraftByIdQuery
    {
        public GetAircraftByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}