using TourCore.Application.Airports.DTOs;

namespace TourCore.Application.Airports.Queries
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