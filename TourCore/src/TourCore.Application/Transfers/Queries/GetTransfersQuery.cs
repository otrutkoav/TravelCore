using TourCore.Application.Transfers.DTOs;

namespace TourCore.Application.Transfers.Queries
{
    public class GetTransfersQuery
    {
        public GetTransfersQuery()
        {
            Filter = new TransferListFilter();
        }

        public TransferListFilter Filter { get; set; }
    }
}