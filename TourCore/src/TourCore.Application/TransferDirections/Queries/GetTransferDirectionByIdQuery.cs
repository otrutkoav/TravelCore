namespace TourCore.Application.TransferDirections.Queries
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