using TourCore.Application.Regions.DTOs;

namespace TourCore.Application.Regions.Queries
{
    public class GetRegionsQuery
    {
        public GetRegionsQuery()
        {
            Filter = new RegionListFilter();
        }

        public RegionListFilter Filter { get; set; }
    }
}