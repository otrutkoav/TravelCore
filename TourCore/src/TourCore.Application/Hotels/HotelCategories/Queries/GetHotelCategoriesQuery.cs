using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.HotelCategories.DTOs;

namespace TourCore.Application.Hotels.HotelCategories.Queries
{
    /// <summary>
    /// Запрос списка категорий отелей.
    /// </summary>
    public class GetHotelCategoriesQuery : PagedQuery
    {
        public GetHotelCategoriesQuery()
        {
            Filter = new HotelCategoryListFilter();
        }

        /// <summary>
        /// Фильтр списка категорий отелей.
        /// </summary>
        public HotelCategoryListFilter Filter { get; set; }
    }
}