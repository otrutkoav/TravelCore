namespace TourCore.Application.Tours
{
    public class GetTourHotelsQuery
    {
        public GetTourHotelsQuery(int tourId, int tourServiceId)
        {
            TourId = tourId;
            TourServiceId = tourServiceId;
        }

        public int TourId { get; }
        public int TourServiceId { get; }
    }
}
