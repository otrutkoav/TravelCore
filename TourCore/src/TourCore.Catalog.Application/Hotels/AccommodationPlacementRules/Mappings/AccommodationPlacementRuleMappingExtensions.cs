using System.Linq;
using TourCore.Contracts.Hotels.AccommodationPlacementRules;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Mappings
{
    public static class AccommodationPlacementRuleMappingExtensions
    {
        public static AccommodationPlacementRuleDto ToDto(this AccommodationPlacementRule entity)
        {
            return new AccommodationPlacementRuleDto
            {
                Id = entity.Id,
                AdultsCount = entity.AdultsCount,
                ChildrenCount = entity.ChildrenCount,
                ChildrenAreInfants = entity.ChildrenAreInfants,
                ChildAgeRanges = entity.ChildAgeRanges
                    .Select(x => x.ToDto())
                    .ToArray()
            };
        }

        public static AccommodationPlacementRuleListItemDto ToListItemDto(this AccommodationPlacementRule entity)
        {
            return new AccommodationPlacementRuleListItemDto
            {
                Id = entity.Id,
                AdultsCount = entity.AdultsCount,
                ChildrenCount = entity.ChildrenCount,
                ChildrenAreInfants = entity.ChildrenAreInfants,
                ChildAgeRanges = entity.ChildAgeRanges
                    .Select(x => x.ToDto())
                    .ToArray()
            };
        }

        public static AccommodationPlacementRuleAgeRangeDto ToDto(this AccommodationPlacementRuleAgeRange entity)
        {
            return new AccommodationPlacementRuleAgeRangeDto
            {
                Id = entity.Id,
                AccommodationPlacementRuleId = entity.AccommodationPlacementRuleId,
                AgeFrom = entity.AgeFrom,
                AgeTo = entity.AgeTo
            };
        }
    }
}