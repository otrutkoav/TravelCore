namespace TourCore.Application.AccommodationTypes.DTOs
{
    public class AccommodationTypeListItemDto
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public bool IsMain { get; set; }
        public short? PerRoom { get; set; }

        public int SortOrder { get; set; }
    }
}