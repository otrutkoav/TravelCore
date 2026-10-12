using TourCore.Application.Common.Queries;
using TourCore.Application.Seating.VehiclePlans.DTOs;

namespace TourCore.Application.Seating.VehiclePlans.Queries
{
    /// <summary>
    /// Запрос списка схем транспорта.
    /// </summary>
    public class GetVehiclePlansQuery : PagedQuery
    {
        public GetVehiclePlansQuery()
        {
            Filter = new VehiclePlanListFilter();
        }

        /// <summary>
        /// Фильтр списка схем транспорта.
        /// </summary>
        public VehiclePlanListFilter Filter { get; set; }
    }
}