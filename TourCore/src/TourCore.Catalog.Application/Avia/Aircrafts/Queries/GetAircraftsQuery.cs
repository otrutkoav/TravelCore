using TourCore.Application.Avia.Aircrafts.DTOs;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Avia.Aircrafts.Queries
{
    /// <summary>
    /// Запрос списка воздушных судов.
    /// </summary>
    public class GetAircraftsQuery : PagedQuery
    {
        public GetAircraftsQuery()
        {
            Filter = new AircraftListFilter();
        }

        /// <summary>
        /// Фильтр списка воздушных судов.
        /// </summary>
        public AircraftListFilter Filter { get; set; }
    }
}