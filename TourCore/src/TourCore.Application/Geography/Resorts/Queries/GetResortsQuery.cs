using TourCore.Application.Geography.Resorts.DTOs;

namespace TourCore.Application.Geography.Resorts.Queries
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