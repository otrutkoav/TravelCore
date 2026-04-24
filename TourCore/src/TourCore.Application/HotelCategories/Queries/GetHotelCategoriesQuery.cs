using TourCore.Application.HotelCategories.DTOs;

namespace TourCore.Application.HotelCategories.Queries
{
    public class GetHotelCategoriesQuery
    {
        public GetHotelCategoriesQuery()
        {
            Filter = new HotelCategoryListFilter();
        }

        public HotelCategoryListFilter Filter { get; set; }
    }
}