using TourCore.Application.AccommodationTypes.DTOs;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.AccommodationTypes.Mappings
{
    public static class AccommodationTypeMappingExtensions
    {
        public static AccommodationTypeDto ToDto(this AccommodationType entity)
        {
            return new AccommodationTypeDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                NameEn = entity.NameEn,
                IsMain = entity.IsMain,
                AgeFrom = entity.AgeFrom,
                AgeTo = entity.AgeTo,
                PerRoom = entity.PerRoom,
                SortOrder = entity.SortOrder,
                Description = entity.Description,
                MainPlacementRule = entity.MainPlacementRule,
                ExtraPlacementRule = entity.ExtraPlacementRule,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static AccommodationTypeListItemDto ToListItemDto(this AccommodationType entity)
        {
            return new AccommodationTypeListItemDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                IsMain = entity.IsMain,
                PerRoom = entity.PerRoom,
                SortOrder = entity.SortOrder
            };
        }
    }
}