using TourCore.Application.Bus.BusTransferPoints.DTOs;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Bus.BusTransferPoints.Queries
{
    /// <summary>
    /// Запрос списка точек автобусных переездов.
    /// </summary>
    public class GetBusTransferPointsQuery : PagedQuery
    {
        public GetBusTransferPointsQuery()
        {
            Filter = new BusTransferPointListFilter();
        }

        /// <summary>
        /// Фильтр списка точек автобусных переездов.
        /// </summary>
        public BusTransferPointListFilter Filter { get; set; }
    }
}