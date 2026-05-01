using TourCore.Application.Hotels.RoomTypes.DTOs;

namespace TourCore.Application.Hotels.RoomTypes.Queries
{
    public class GetRoomTypesQuery
    {
        public GetRoomTypesQuery()
        {
            Filter = new RoomTypeListFilter();
        }

        public RoomTypeListFilter Filter { get; set; }
    }
}