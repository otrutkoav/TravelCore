using TourCore.Application.Hotels.HotelRoomCombinations.DTOs;

namespace TourCore.Application.Hotels.HotelRoomCombinations.Queries
{
    public class GetHotelRoomCombinationsQuery
    {
        public GetHotelRoomCombinationsQuery()
        {
            Filter = new HotelRoomCombinationListFilter();
        }

        public HotelRoomCombinationListFilter Filter { get; set; }
    }
}