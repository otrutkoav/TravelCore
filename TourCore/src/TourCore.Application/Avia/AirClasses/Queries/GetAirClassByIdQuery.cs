namespace TourCore.Application.Avia.AirClasses.Queries
{
    public class GetAirClassByIdQuery
    {
        public GetAirClassByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}