namespace TourCore.Application.Hotels.RoomCategories.Commands
{
    public class UpdateRoomCategoryCommand
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }

        public int SortOrder { get; set; }

        public string Description { get; set; }
    }
}