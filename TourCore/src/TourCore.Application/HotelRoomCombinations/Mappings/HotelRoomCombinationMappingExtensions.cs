using TourCore.Contracts.Hotels.HotelRoomCombinations;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.HotelRoomCombinations.Mappings
{
    public static class HotelRoomCombinationMappingExtensions
    {
        public static HotelRoomCombinationDto ToDto(this HotelRoomCombination entity)
        {
            return new HotelRoomCombinationDto
            {
                Id = entity.Id,
                RoomTypeId = entity.RoomTypeId,
                RoomCategoryId = entity.RoomCategoryId,
                AccommodationTypeId = entity.AccommodationTypeId,
                IsMain = entity.IsMain,
                AgeFrom = entity.AgeFrom,
                AgeTo = entity.AgeTo
            };
        }

        public static HotelRoomCombinationListItemDto ToListItemDto(this HotelRoomCombination entity)
        {
            return new HotelRoomCombinationListItemDto
            {
                Id = entity.Id,
                RoomTypeId = entity.RoomTypeId,
                RoomCategoryId = entity.RoomCategoryId,
                AccommodationTypeId = entity.AccommodationTypeId,
                IsMain = entity.IsMain,
                AgeFrom = entity.AgeFrom,
                AgeTo = entity.AgeTo
            };
        }
    }
}