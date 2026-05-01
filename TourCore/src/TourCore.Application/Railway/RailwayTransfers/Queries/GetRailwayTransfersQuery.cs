using TourCore.Application.Railway.RailwayTransfers.DTOs;

namespace TourCore.Application.Railway.RailwayTransfers.Queries
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