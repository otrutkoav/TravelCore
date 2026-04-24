namespace TourCore.Application.HotelRoomCombinations.Commands
{
    public class CreateHotelRoomCombinationCommand
    {
        public int RoomTypeId { get; set; }
        public int RoomCategoryId { get; set; }
        public int AccommodationTypeId { get; set; }

        public bool IsMain { get; set; }

        public short? AgeFrom { get; set; }
        public short? AgeTo { get; set; }
    }
}