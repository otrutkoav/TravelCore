using TourCore.Application.Avia.Airports.DTOs;

namespace TourCore.Application.Avia.Airports.Queries
{
    public class GetAirportsQuery
    {
        public GetAirportsQuery()
        {
            Filter = new AirportListFilter();
        }

        public AirportListFilter Filter { get; set; }
    }
}