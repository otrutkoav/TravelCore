using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.RoomTypes.DTOs;

namespace TourCore.Application.Hotels.RoomTypes.Queries
{
    /// <summary>
    /// Запрос списка типов номеров.
    /// </summary>
    public class GetRoomTypesQuery : PagedQuery
    {
        public GetRoomTypesQuery()
        {
            Filter = new RoomTypeListFilter();
        }

        /// <summary>
        /// Фильтр списка типов номеров.
        /// </summary>
        public RoomTypeListFilter Filter { get; set; }
    }
}