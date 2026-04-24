using TourCore.Application.RailwayTransfers.DTOs;

namespace TourCore.Application.RailwayTransfers.Queries
{
    public class GetRailwayTransfersQuery
    {
        public GetRailwayTransfersQuery()
        {
            Filter = new RailwayTransferListFilter();
        }

        public RailwayTransferListFilter Filter { get; set; }
    }
}