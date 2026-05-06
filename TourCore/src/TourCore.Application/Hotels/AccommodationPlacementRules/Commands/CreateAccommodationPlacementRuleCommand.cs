namespace TourCore.Application.Hotels.AccommodationPlacementRules.Commands
{
    public class CreateAccommodationPlacementRuleCommand
    {
        public short AdultsCount { get; set; }

        public short ChildrenCount { get; set; }

        public bool ChildrenAreInfants { get; set; }
    }
}