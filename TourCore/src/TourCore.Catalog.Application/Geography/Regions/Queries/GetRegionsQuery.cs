using TourCore.Application.Common.Queries;
using TourCore.Application.Geography.Regions.DTOs;

namespace TourCore.Application.Geography.Regions.Queries
{
    /// <summary>
    /// Запрос списка регионов.
    /// </summary>
    public class GetRegionsQuery : PagedQuery
    {
        public GetRegionsQuery()
        {
            Filter = new RegionListFilter();
        }

        /// <summary>
        /// Фильтр списка регионов.
        /// </summary>
        public RegionListFilter Filter { get; set; }
    }
}