using TourCore.Application.Common.Queries;
using TourCore.Application.Hotels.AccommodationTypes.DTOs;

namespace TourCore.Application.Hotels.AccommodationTypes.Queries
{
    /// <summary>
    /// Запрос списка типов размещения.
    /// </summary>
    public class GetAccommodationTypesQuery : PagedQuery
    {
        public GetAccommodationTypesQuery()
        {
            Filter = new AccommodationTypeListFilter();
        }

        /// <summary>
        /// Фильтр списка типов размещения.
        /// </summary>
        public AccommodationTypeListFilter Filter { get; set; }
    }
}