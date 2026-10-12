namespace TourCore.Application.Bus.BusTransfers.Queries
{
    public class GetBusTransferByIdQuery
    {
        public GetBusTransferByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}