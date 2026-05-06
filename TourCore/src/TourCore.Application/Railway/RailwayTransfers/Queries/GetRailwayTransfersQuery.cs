using TourCore.Application.Common.Queries;
using TourCore.Application.Railway.RailwayTransfers.DTOs;

namespace TourCore.Application.Railway.RailwayTransfers.Queries
{
    /// <summary>
    /// Запрос списка железнодорожных переездов.
    /// </summary>
    public class GetRailwayTransfersQuery : PagedQuery
    {
        public GetRailwayTransfersQuery()
        {
            Filter = new RailwayTransferListFilter();
        }

        /// <summary>
        /// Фильтр списка железнодорожных переездов.
        /// </summary>
        public RailwayTransferListFilter Filter { get; set; }
    }
}