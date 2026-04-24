namespace TourCore.Application.BusTransferPoints.Queries
{
    public class GetBusTransferPointByIdQuery
    {
        public GetBusTransferPointByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}