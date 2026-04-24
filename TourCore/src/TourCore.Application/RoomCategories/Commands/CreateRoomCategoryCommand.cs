namespace TourCore.Application.RoomCategories.Commands
{
    public class CreateRoomCategoryCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

        public int SortOrder { get; set; }
        public string Description { get; set; }
    }
}