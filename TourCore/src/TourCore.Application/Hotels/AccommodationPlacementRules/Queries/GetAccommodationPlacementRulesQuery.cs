using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.AccommodationPlacementRules.DTOs;

namespace TourCore.Application.Hotels.AccommodationPlacementRules.Queries
{
    /// <summary>
    /// Запрос списка правил размещения.
    /// </summary>
    public class GetAccommodationPlacementRulesQuery : PagedQuery
    {
        public GetAccommodationPlacementRulesQuery()
        {
            Filter = new AccommodationPlacementRuleListFilter();
        }

        /// <summary>
        /// Фильтр списка правил размещения.
        /// </summary>
        public AccommodationPlacementRuleListFilter Filter { get; set; }
    }
}