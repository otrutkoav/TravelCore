using TourCore.Application.Common.Queries;
using TourCore.Application.Transportation.Transports.DTOs;

namespace TourCore.Application.Transportation.Transports.Queries
{
    /// <summary>
    /// Запрос списка транспорта.
    /// </summary>
    public class GetTransportsQuery : PagedQuery
    {
        public GetTransportsQuery()
        {
            Filter = new TransportListFilter();
        }

        /// <summary>
        /// Фильтр списка транспорта.
        /// </summary>
        public TransportListFilter Filter { get; set; }
    }
}