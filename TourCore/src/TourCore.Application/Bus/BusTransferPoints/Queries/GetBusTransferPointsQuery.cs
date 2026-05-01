using TourCore.Application.Bus.BusTransferPoints.DTOs;

namespace TourCore.Application.Bus.BusTransferPoints.Queries
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