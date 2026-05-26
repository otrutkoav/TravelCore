using TourCore.Application.Avia.Charters.DTOs;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Avia.Charters.Queries
{
    /// <summary>
    /// Запрос списка чартерных рейсов.
    /// </summary>
    public class GetChartersQuery : PagedQuery
    {
        public GetChartersQuery()
        {
            Filter = new CharterListFilter();
        }

        /// <summary>
        /// Фильтр списка чартерных рейсов.
        /// </summary>
        public CharterListFilter Filter { get; set; }
    }
}