using TourCore.Application.Geography.Regions.DTOs;

namespace TourCore.Application.Geography.Regions.Queries
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