using TourCore.Application.Avia.Airlines.DTOs;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Avia.Airlines.Queries
{
    /// <summary>
    /// Запрос списка авиакомпаний.
    /// </summary>
    public class GetAirlinesQuery : PagedQuery
    {
        public GetAirlinesQuery()
        {
            Filter = new AirlineListFilter();
        }

        /// <summary>
        /// Фильтр списка авиакомпаний.
        /// </summary>
        public AirlineListFilter Filter { get; set; }
    }
}