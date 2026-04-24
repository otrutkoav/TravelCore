using TourCore.Application.AirClasses.DTOs;

namespace TourCore.Application.AirClasses.Queries
{
    public class GetAirClassesQuery
    {
        public GetAirClassesQuery()
        {
            Filter = new AirClassListFilter();
        }

        public AirClassListFilter Filter { get; set; }
    }
}