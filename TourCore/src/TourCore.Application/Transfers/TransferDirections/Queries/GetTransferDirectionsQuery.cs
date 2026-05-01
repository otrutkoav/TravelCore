using TourCore.Application.Transfers.TransferDirections.DTOs;

namespace TourCore.Application.Transfers.TransferDirections.Queries
{
    public class GetTransferDirectionsQuery
    {
        public GetTransferDirectionsQuery()
        {
            Filter = new TransferDirectionListFilter();
        }

        public TransferDirectionListFilter Filter { get; set; }
    }
}