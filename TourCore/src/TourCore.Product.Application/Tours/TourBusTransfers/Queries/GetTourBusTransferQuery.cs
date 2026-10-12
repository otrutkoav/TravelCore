namespace TourCore.Application.Tours
{
    public class GetTourBusTransferQuery
    {
        public GetTourBusTransferQuery(int tourId, int tourServiceId)
        {
            TourId = tourId;
            TourServiceId = tourServiceId;
        }

        public int TourId { get; }
        public int TourServiceId { get; }
    }
}
