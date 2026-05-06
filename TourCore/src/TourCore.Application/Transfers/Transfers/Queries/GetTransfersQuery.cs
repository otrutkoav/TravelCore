using TourCore.Application.Common.Queries;
using TourCore.Application.Transfers.Transfers.DTOs;

namespace TourCore.Application.Transfers.Transfers.Queries
{
    /// <summary>
    /// Запрос списка трансферов.
    /// </summary>
    public class GetTransfersQuery : PagedQuery
    {
        public GetTransfersQuery()
        {
            Filter = new TransferListFilter();
        }

        /// <summary>
        /// Фильтр списка трансферов.
        /// </summary>
        public TransferListFilter Filter { get; set; }
    }
}