using TourCore.Application.Hotels.DTOs;

namespace TourCore.Application.Hotels.Queries
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