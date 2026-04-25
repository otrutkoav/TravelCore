using System.Linq;
using TourCore.Contracts.Hotels.AccommodationTypes;
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
                MainPlacementRule = MapRule(entity.MainPlacementRule),
                ExtraPlacementRule = MapRule(entity.ExtraPlacementRule)
            };
        }

        public static AccommodationTypeListItemDto ToListItemDto(this AccommodationType entity)
        {
            return new AccommodationTypeListItemDto
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
                MainPlacementRule = MapRule(entity.MainPlacementRule),
                ExtraPlacementRule = MapRule(entity.ExtraPlacementRule)
            };
        }

        private static AccommodationPlacementRuleDto MapRule(
            Domain.Hotels.ValueObjects.AccommodationPlacementRule rule)
        {
            if (rule == null)
                return null;

            return new AccommodationPlacementRuleDto
            {
                AdultsCount = rule.AdultsCount,
                ChildrenCount = rule.ChildrenCount,
                ChildrenAreInfants = rule.ChildrenAreInfants,
                ChildAgeRanges = rule.ChildAgeRanges?
                    .Select(x => new AgeRangeDto
                    {
                        AgeFrom = x.AgeFrom,
                        AgeTo = x.AgeTo
                    })
                    .ToList()
            };
        }
    }
}