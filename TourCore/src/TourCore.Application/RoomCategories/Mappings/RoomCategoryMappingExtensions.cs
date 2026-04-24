using TourCore.Application.RoomCategories.DTOs;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.RoomCategories.Mappings
{
    public static class RoomCategoryMappingExtensions
    {
        public static RoomCategoryDto ToDto(this RoomCategory entity)
        {
            return new RoomCategoryDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                NameEn = entity.NameEn,
                SortOrder = entity.SortOrder,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static RoomCategoryListItemDto ToListItemDto(this RoomCategory entity)
        {
            return new RoomCategoryListItemDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                SortOrder = entity.SortOrder
            };
        }
    }
}