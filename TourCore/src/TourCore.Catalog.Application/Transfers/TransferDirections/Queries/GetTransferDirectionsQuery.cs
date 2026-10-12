using TourCore.Application.Common.Queries;
using TourCore.Application.Transfers.TransferDirections.DTOs;

namespace TourCore.Application.Transfers.TransferDirections.Queries
{
    /// <summary>
    /// Запрос списка направлений трансферов.
    /// </summary>
    public class GetTransferDirectionsQuery : PagedQuery
    {
        public GetTransferDirectionsQuery()
        {
            Filter = new TransferDirectionListFilter();
        }

        /// <summary>
        /// Фильтр списка направлений трансферов.
        /// </summary>
        public TransferDirectionListFilter Filter { get; set; }
    }
}