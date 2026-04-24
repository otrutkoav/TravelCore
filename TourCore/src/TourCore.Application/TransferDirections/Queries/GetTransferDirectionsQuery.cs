using TourCore.Application.TransferDirections.DTOs;

namespace TourCore.Application.TransferDirections.Queries
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