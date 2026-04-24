using TourCore.Application.BusTransferPoints.DTOs;

namespace TourCore.Application.BusTransferPoints.Queries
{
    public class GetBusTransferPointsQuery
    {
        public GetBusTransferPointsQuery()
        {
            Filter = new BusTransferPointListFilter();
        }

        public BusTransferPointListFilter Filter { get; set; }
    }
}