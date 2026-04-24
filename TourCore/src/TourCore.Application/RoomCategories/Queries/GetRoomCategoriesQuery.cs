using TourCore.Application.RoomCategories.DTOs;

namespace TourCore.Application.RoomCategories.Queries
{
    public class GetRoomCategoriesQuery
    {
        public GetRoomCategoriesQuery()
        {
            Filter = new RoomCategoryListFilter();
        }

        public RoomCategoryListFilter Filter { get; set; }
    }
}