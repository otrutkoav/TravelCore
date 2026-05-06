using TourCore.Application.Hotels.AccommodationPlacementRules.Mappings;
using TourCore.Contracts.Hotels.AccommodationTypes;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.AccommodationTypes.Mappings
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

                MainPlacementRuleId = entity.MainPlacementRuleId,
                MainPlacementRule = entity.MainPlacementRule?.ToDto(),

                ExtraPlacementRuleId = entity.ExtraPlacementRuleId,
                ExtraPlacementRule = entity.ExtraPlacementRule?.ToDto()
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
                SortOrder = entity.SortOrder,
                MainPlacementRuleId = entity.MainPlacementRuleId,
                ExtraPlacementRuleId = entity.ExtraPlacementRuleId
            };
        }
    }
}