using TourCore.Application.Hotels.HotelCategories.DTOs;

namespace TourCore.Application.Hotels.HotelCategories.Queries
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