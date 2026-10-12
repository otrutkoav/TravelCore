namespace TourCore.Application.Tours
{
    public class GetTourRailwayTransferQuery
    {
        public GetTourRailwayTransferQuery(int tourId, int tourServiceId)
        {
            TourId = tourId;
            TourServiceId = tourServiceId;
        }

        public int TourId { get; }
        public int TourServiceId { get; }
    }
}
