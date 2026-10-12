using TourCore.Application.Bus.BusTransfers.DTOs;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Bus.BusTransfers.Queries
{
    /// <summary>
    /// Запрос списка автобусных переездов.
    /// </summary>
    public class GetBusTransfersQuery : PagedQuery
    {
        public GetBusTransfersQuery()
        {
            Filter = new BusTransferListFilter();
        }

        /// <summary>
        /// Фильтр списка автобусных переездов.
        /// </summary>
        public BusTransferListFilter Filter { get; set; }
    }
}