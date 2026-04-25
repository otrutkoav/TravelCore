using System.Collections.Generic;

namespace TourCore.Contracts.Hotels.AccommodationTypes
{
    public class AccommodationPlacementRuleDto
    {
        public short AdultsCount { get; set; }

        public short ChildrenCount { get; set; }

        public bool ChildrenAreInfants { get; set; }

        public IReadOnlyCollection<AgeRangeDto> ChildAgeRanges { get; set; }
    }
}