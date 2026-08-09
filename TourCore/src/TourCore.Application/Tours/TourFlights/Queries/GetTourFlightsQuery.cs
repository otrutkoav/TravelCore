namespace TourCore.Application.Tours
{
    public class GetTourFlightsQuery
    {
        public GetTourFlightsQuery(int tourId, int tourServiceId)
        {
            TourId = tourId;
            TourServiceId = tourServiceId;
        }

        public int TourId { get; }
        public int TourServiceId { get; }
    }
}
