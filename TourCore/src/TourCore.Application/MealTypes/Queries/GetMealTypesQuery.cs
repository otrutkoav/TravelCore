using TourCore.Application.MealTypes.DTOs;

namespace TourCore.Application.MealTypes.Queries
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