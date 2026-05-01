namespace TourCore.Application.Transportation.Transports.Queries
{
    public class GetTransportByIdQuery
    {
        public GetTransportByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}