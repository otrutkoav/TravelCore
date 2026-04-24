using TourCore.Application.HotelRoomCombinations.DTOs;

namespace TourCore.Application.HotelRoomCombinations.Queries
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