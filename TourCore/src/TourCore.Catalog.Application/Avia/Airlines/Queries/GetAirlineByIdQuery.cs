namespace TourCore.Application.Avia.Airlines.Queries
{
    public class GetAirlineByIdQuery
    {
        public GetAirlineByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}