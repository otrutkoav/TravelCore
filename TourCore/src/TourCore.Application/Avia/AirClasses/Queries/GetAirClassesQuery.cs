using TourCore.Application.Avia.AirClasses.DTOs;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Avia.AirClasses.Queries
{
    /// <summary>
    /// Запрос списка классов обслуживания.
    /// </summary>
    public class GetAirClassesQuery : PagedQuery
    {
        public GetAirClassesQuery()
        {
            Filter = new AirClassListFilter();
        }

        /// <summary>
        /// Фильтр списка классов обслуживания.
        /// </summary>
        public AirClassListFilter Filter { get; set; }
    }
}