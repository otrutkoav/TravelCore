using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.Hotels.DTOs;

namespace TourCore.Application.Hotels.Hotels.Queries
{
    /// <summary>
    /// Запрос списка отелей.
    /// </summary>
    public class GetHotelsQuery : PagedQuery
    {
        public GetHotelsQuery()
        {
            Filter = new HotelListFilter();
        }

        /// <summary>
        /// Фильтр списка отелей.
        /// </summary>
        public HotelListFilter Filter { get; set; }
    }
}