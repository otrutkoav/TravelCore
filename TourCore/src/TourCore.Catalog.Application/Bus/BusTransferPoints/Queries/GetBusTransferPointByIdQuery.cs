namespace TourCore.Application.Bus.BusTransferPoints.Queries
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