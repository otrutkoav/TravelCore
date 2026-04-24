namespace TourCore.Application.RoomTypes.DTOs
{
    public class RoomTypeListItemDto
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public short? Places { get; set; }
        public short? ExtraPlaces { get; set; }

        public int SortOrder { get; set; }
    }
}