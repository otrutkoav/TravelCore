namespace TourCore.Application.Transfers.Transfers.Queries
{
    public class GetTransferByIdQuery
    {
        public GetTransferByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}