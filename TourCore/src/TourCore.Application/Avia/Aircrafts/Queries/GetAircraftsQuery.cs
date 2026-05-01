using TourCore.Application.Avia.Aircrafts.DTOs;

namespace TourCore.Application.Avia.Aircrafts.Queries
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