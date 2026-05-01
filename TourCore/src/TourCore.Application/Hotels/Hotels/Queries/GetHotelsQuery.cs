using TourCore.Application.Hotels.Hotels.DTOs;

namespace TourCore.Application.Hotels.Hotels.Queries
{
    public class GetHotelsQuery
    {
        public GetHotelsQuery()
        {
            Filter = new HotelListFilter();
        }

        public HotelListFilter Filter { get; set; }
    }
}