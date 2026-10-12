using TourCore.Application.Common.Queries;
using TourCore.Application.Finance.Rates.DTOs;

namespace TourCore.Application.Finance.Rates.Queries
{
    /// <summary>
    /// Запрос списка валют.
    /// </summary>
    public class GetRatesQuery : PagedQuery
    {
        public GetRatesQuery()
        {
            Filter = new RateListFilter();
        }

        /// <summary>
        /// Фильтр списка валют.
        /// </summary>
        public RateListFilter Filter { get; set; }
    }
}