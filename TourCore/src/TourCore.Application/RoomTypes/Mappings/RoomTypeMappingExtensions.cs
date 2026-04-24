using TourCore.Application.RoomTypes.DTOs;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.RoomTypes.Mappings
{
    public static class RoomTypeMappingExtensions
    {
        public static RoomTypeDto ToDto(this RoomType entity)
        {
            return new RoomTypeDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                NameEn = entity.NameEn,
                Places = entity.Places,
                ExtraPlaces = entity.ExtraPlaces,
                SortOrder = entity.SortOrder,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static RoomTypeListItemDto ToListItemDto(this RoomType entity)
        {
            return new RoomTypeListItemDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Places = entity.Places,
                ExtraPlaces = entity.ExtraPlaces,
                SortOrder = entity.SortOrder
            };
        }
    }
}