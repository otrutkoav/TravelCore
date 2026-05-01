namespace TourCore.Application.Avia.Airports.Queries
{
    public class GetAirportByIdQuery
    {
        public GetAirportByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}