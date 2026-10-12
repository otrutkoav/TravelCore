namespace TourCore.Application.Transfers.TransferDirections.Queries
{
    public class GetTransferDirectionByIdQuery
    {
        public GetTransferDirectionByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}