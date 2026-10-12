using TourCore.Application.Bus.BusSchedules.DTOs;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Bus.BusSchedules.Queries
{
    /// <summary>
    /// Запрос списка расписаний автобусных переездов.
    /// </summary>
    public class GetBusSchedulesQuery : PagedQuery
    {
        public GetBusSchedulesQuery()
        {
            Filter = new BusScheduleListFilter();
        }

        /// <summary>
        /// Фильтр списка расписаний автобусных переездов.
        /// </summary>
        public BusScheduleListFilter Filter { get; set; }
    }
}