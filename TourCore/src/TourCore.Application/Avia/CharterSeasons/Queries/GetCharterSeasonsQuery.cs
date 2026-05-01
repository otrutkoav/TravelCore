using TourCore.Application.Avia.CharterSeasons.DTOs;

namespace TourCore.Application.Avia.CharterSeasons.Queries
{
    public class GetCharterSeasonsQuery
    {
        public GetCharterSeasonsQuery()
        {
            Filter = new CharterSeasonListFilter();
        }

        public CharterSeasonListFilter Filter { get; set; }
    }
}