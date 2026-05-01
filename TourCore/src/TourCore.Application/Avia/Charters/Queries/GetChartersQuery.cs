using TourCore.Application.Avia.Charters.DTOs;

namespace TourCore.Application.Avia.Charters.Queries
{
    public class GetChartersQuery
    {
        public GetChartersQuery()
        {
            Filter = new CharterListFilter();
        }

        public CharterListFilter Filter { get; set; }
    }
}