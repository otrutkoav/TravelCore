using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.MealTypes.DTOs;

namespace TourCore.Application.Hotels.MealTypes.Queries
{
    /// <summary>
    /// Запрос списка типов питания.
    /// </summary>
    public class GetMealTypesQuery : PagedQuery
    {
        public GetMealTypesQuery()
        {
            Filter = new MealTypeListFilter();
        }

        /// <summary>
        /// Фильтр списка типов питания.
        /// </summary>
        public MealTypeListFilter Filter { get; set; }
    }
}