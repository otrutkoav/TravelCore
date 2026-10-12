using TourCore.Application.Common.Queries;
using TourCore.Application.Geography.Resorts.DTOs;

namespace TourCore.Application.Geography.Resorts.Queries
{
    /// <summary>
    /// Запрос списка курортов.
    /// </summary>
    public class GetResortsQuery : PagedQuery
    {
        public GetResortsQuery()
        {
            Filter = new ResortListFilter();
        }

        /// <summary>
        /// Фильтр списка курортов.
        /// </summary>
        public ResortListFilter Filter { get; set; }
    }
}