using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Queries
{
    internal static class AccommodationPlacementRuleSortDefinition
    {
        public static readonly SortDefinition<AccommodationPlacementRule> Instance =
            new SortDefinition<AccommodationPlacementRule>(
                new SortExpression<AccommodationPlacementRule>[]
                {
                    new SortExpression<AccommodationPlacementRule, int>("id", x => x.Id),
                    new SortExpression<AccommodationPlacementRule, short>("adultsCount", x => x.AdultsCount),
                    new SortExpression<AccommodationPlacementRule, short>("childrenCount", x => x.ChildrenCount),
                    new SortExpression<AccommodationPlacementRule, bool>("childrenAreInfants", x => x.ChildrenAreInfants)
                },
                (Expression<Func<AccommodationPlacementRule, short>>)(x => x.AdultsCount));
    }
}