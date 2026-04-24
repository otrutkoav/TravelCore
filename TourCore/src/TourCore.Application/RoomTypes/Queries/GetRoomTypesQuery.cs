using TourCore.Application.RoomTypes.DTOs;

namespace TourCore.Application.RoomTypes.Queries
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