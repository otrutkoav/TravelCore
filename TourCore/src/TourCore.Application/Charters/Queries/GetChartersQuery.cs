using TourCore.Application.Charters.DTOs;

namespace TourCore.Application.Charters.Queries
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