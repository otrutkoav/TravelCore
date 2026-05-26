using TourCore.Application.Avia.Airports.DTOs;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Avia.Airports.Queries
{
    /// <summary>
    /// Запрос списка аэропортов.
    /// </summary>
    public class GetAirportsQuery : PagedQuery
    {
        public GetAirportsQuery()
        {
            Filter = new AirportListFilter();
        }

        /// <summary>
        /// Фильтр списка аэропортов.
        /// </summary>
        public AirportListFilter Filter { get; set; }
    }
}