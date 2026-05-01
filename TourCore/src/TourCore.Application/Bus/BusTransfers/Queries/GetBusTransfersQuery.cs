using TourCore.Application.Bus.BusTransfers.DTOs;

namespace TourCore.Application.Bus.BusTransfers.Queries
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