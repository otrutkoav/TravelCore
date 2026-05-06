using TourCore.Application.Hotels.AccommodationPlacementRules.DTOs;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Queries
{
    public class GetAccommodationPlacementRulesQuery
    {
        public GetAccommodationPlacementRulesQuery()
        {
            Filter = new AccommodationPlacementRuleListFilter();
        }

        public AccommodationPlacementRuleListFilter Filter { get; set; }
    }
}