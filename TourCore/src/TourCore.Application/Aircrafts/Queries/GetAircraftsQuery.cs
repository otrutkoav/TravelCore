using TourCore.Application.Aircrafts.DTOs;

namespace TourCore.Application.Aircrafts.Queries
{
    public class GetAircraftsQuery
    {
        public GetAircraftsQuery()
        {
            Filter = new AircraftListFilter();
        }

        public AircraftListFilter Filter { get; set; }
    }
}