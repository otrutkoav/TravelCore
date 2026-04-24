namespace TourCore.Application.HotelRoomCombinations.DTOs
{
    public class HotelRoomCombinationListItemDto
    {
        public int Id { get; set; }

        public int RoomTypeId { get; set; }
        public int RoomCategoryId { get; set; }
        public int AccommodationTypeId { get; set; }

        public bool IsMain { get; set; }
    }
}