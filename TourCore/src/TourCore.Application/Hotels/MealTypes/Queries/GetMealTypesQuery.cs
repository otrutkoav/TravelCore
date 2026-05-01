using TourCore.Application.Hotels.MealTypes.DTOs;

namespace TourCore.Application.Hotels.MealTypes.Queries
{
    public class GetMealTypesQuery
    {
        public GetMealTypesQuery()
        {
            Filter = new MealTypeListFilter();
        }

        public MealTypeListFilter Filter { get; set; }
    }
}