namespace TourCore.Application.Hotels.AccommodationPlacementRules.DTOs
{
    public class AccommodationPlacementRuleListFilter
    {
        public short? AdultsCount { get; set; }

        public short? ChildrenCount { get; set; }

        public bool? ChildrenAreInfants { get; set; }
    }
}