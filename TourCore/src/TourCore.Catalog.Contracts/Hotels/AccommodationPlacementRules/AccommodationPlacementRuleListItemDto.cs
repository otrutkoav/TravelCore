using System.Collections.Generic;

namespace TourCore.Contracts.Hotels.AccommodationPlacementRules
{
    public class AccommodationPlacementRuleListItemDto
    {
        public int Id { get; set; }

        public short AdultsCount { get; set; }

        public short ChildrenCount { get; set; }

        public bool ChildrenAreInfants { get; set; }

        public IReadOnlyCollection<AccommodationPlacementRuleAgeRangeDto> ChildAgeRanges { get; set; }
    }
}