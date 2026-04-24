using System;

namespace TourCore.Application.HotelRoomCombinations.DTOs
{
    public class HotelRoomCombinationDto
    {
        public int Id { get; set; }

        public int RoomTypeId { get; set; }
        public int RoomCategoryId { get; set; }
        public int AccommodationTypeId { get; set; }

        public bool IsMain { get; set; }

        public short? AgeFrom { get; set; }
        public short? AgeTo { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}