using TourCore.Application.Common.Queries;
using TourCore.Application.Geography.Countries.DTOs;

namespace TourCore.Application.Geography.Countries.Queries
{
    /// <summary>
    /// Запрос списка стран.
    /// </summary>
    public class GetCountriesQuery : PagedQuery
    {
        public GetCountriesQuery()
        {
            Filter = new CountryListFilter();
        }

        /// <summary>
        /// Фильтр списка стран.
        /// </summary>
        public CountryListFilter Filter { get; set; }
    }
}