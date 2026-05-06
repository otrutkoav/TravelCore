namespace TourCore.Contracts.Hotels.AccommodationPlacementRules
{
    public class AccommodationPlacementRuleAgeRangeDto
    {
        public int Id { get; set; }

        public int AccommodationPlacementRuleId { get; set; }

        public short AgeFrom { get; set; }

        public short AgeTo { get; set; }
    }
}