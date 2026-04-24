namespace TourCore.Application.HotelRoomCombinations.DTOs
{
    public class HotelRoomCombinationListFilter
    {
        public int? RoomTypeId { get; set; }
        public int? RoomCategoryId { get; set; }
        public int? AccommodationTypeId { get; set; }
        public bool? IsMain { get; set; }
    }
}