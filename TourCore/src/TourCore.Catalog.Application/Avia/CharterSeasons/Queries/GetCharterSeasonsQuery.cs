using TourCore.Application.Avia.CharterSeasons.DTOs;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Avia.CharterSeasons.Queries
{
    /// <summary>
    /// Запрос списка сезонов чартерных рейсов.
    /// </summary>
    public class GetCharterSeasonsQuery : PagedQuery
    {
        public GetCharterSeasonsQuery()
        {
            Filter = new CharterSeasonListFilter();
        }

        /// <summary>
        /// Фильтр списка сезонов чартерных рейсов.
        /// </summary>
        public CharterSeasonListFilter Filter { get; set; }
    }
}