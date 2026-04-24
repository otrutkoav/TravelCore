using TourCore.Application.CharterSeasons.DTOs;

namespace TourCore.Application.CharterSeasons.Queries
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