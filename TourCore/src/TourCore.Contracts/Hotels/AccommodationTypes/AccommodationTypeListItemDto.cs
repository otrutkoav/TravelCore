namespace TourCore.Contracts.Hotels.AccommodationTypes
{
    public class AccommodationTypeListItemDto
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

        public bool IsMain { get; set; }

        public short? AgeFrom { get; set; }
        public short? AgeTo { get; set; }

        public short? PerRoom { get; set; }

        public int SortOrder { get; set; }

        public string Description { get; set; }

        public AccommodationPlacementRuleDto MainPlacementRule { get; set; }

        public AccommodationPlacementRuleDto ExtraPlacementRule { get; set; }
    }
}