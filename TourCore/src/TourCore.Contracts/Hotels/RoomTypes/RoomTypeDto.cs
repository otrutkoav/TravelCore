namespace TourCore.Contracts.Hotels.RoomTypes
{
    public class RoomTypeDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        public short? Places { get; set; }
        public short? ExtraPlaces { get; set; }

        public int SortOrder { get; set; }

        public string Description { get; set; }
    }
}