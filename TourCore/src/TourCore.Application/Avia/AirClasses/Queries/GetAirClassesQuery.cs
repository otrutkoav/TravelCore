using TourCore.Application.Avia.AirClasses.DTOs;

namespace TourCore.Application.Avia.AirClasses.Queries
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