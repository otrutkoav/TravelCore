using TourCore.Application.Transfers.Transfers.DTOs;

namespace TourCore.Application.Transfers.Transfers.Queries
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