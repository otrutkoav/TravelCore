using TourCore.Application.BusTransfers.DTOs;

namespace TourCore.Application.BusTransfers.Queries
{
    public class GetBusTransfersQuery
    {
        public GetBusTransfersQuery()
        {
            Filter = new BusTransferListFilter();
        }

        public BusTransferListFilter Filter { get; set; }
    }
}