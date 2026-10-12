using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.RoomCategories.DTOs;

namespace TourCore.Application.Hotels.RoomCategories.Queries
{
    /// <summary>
    /// Запрос списка категорий номеров.
    /// </summary>
    public class GetRoomCategoriesQuery : PagedQuery
    {
        public GetRoomCategoriesQuery()
        {
            Filter = new RoomCategoryListFilter();
        }

        /// <summary>
        /// Фильтр списка категорий номеров.
        /// </summary>
        public RoomCategoryListFilter Filter { get; set; }
    }
}