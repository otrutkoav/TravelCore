namespace TourCore.Application.RoomCategories.DTOs
{
    public class RoomCategoryListItemDto
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public int SortOrder { get; set; }
    }
}