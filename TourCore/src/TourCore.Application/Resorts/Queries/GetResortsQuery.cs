using TourCore.Application.Resorts.DTOs;

namespace TourCore.Application.Resorts.Queries
{
    public class GetResortsQuery
    {
        public GetResortsQuery()
        {
            Filter = new ResortListFilter();
        }

        public ResortListFilter Filter { get; set; }
    }
}