using System;
using System.Linq;
using System.Linq.Expressions;
using TourCore.Contracts.Hotels.AccommodationPlacementRules;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Queries
{
    internal static class AccommodationPlacementRuleProjections
    {
        public static readonly Expression<Func<AccommodationPlacementRule, AccommodationPlacementRuleListItemDto>> ListItem =
            x => new AccommodationPlacementRuleListItemDto
            {
                Id = x.Id,
                AdultsCount = x.AdultsCount,
                ChildrenCount = x.ChildrenCount,
                ChildrenAreInfants = x.ChildrenAreInfants,
                ChildAgeRanges = x.ChildAgeRanges
                    .Select(r => new AccommodationPlacementRuleAgeRangeDto
                    {
                        Id = r.Id,
                        AccommodationPlacementRuleId = r.AccommodationPlacementRuleId,
                        AgeFrom = r.AgeFrom,
                        AgeTo = r.AgeTo
                    })
                    .ToList()
            };
    }
}