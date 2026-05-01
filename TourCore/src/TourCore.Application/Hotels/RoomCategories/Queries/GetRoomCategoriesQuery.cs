using TourCore.Application.Hotels.RoomCategories.DTOs;

namespace TourCore.Application.Hotels.RoomCategories.Queries
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