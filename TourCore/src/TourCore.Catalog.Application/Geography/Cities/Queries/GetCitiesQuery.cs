using TourCore.Application.Common.Queries;
using TourCore.Application.Geography.Cities.DTOs;

namespace TourCore.Application.Geography.Cities.Queries
{
    /// <summary>
    /// Запрос списка городов.
    /// </summary>
    public class GetCitiesQuery : PagedQuery
    {
        public GetCitiesQuery()
        {
            Filter = new CityListFilter();
        }

        /// <summary>
        /// Фильтр списка городов.
        /// </summary>
        public CityListFilter Filter { get; set; }
    }
}