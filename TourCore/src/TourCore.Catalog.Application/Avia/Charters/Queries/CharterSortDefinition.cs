using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.Charters.Queries
{
    internal static class CharterSortDefinition
    {
        public static readonly SortDefinition<Charter> Instance =
            new SortDefinition<Charter>(
                new SortExpression<Charter>[]
                {
                    new SortExpression<Charter, int>("id", x => x.Id),
                    new SortExpression<Charter, int>("departureCityId", x => x.DepartureCityId),
                    new SortExpression<Charter, string>("departureAirportCode", x => x.DepartureAirportCode),
                    new SortExpression<Charter, int>("arrivalCityId", x => x.ArrivalCityId),
                    new SortExpression<Charter, string>("arrivalAirportCode", x => x.ArrivalAirportCode),
                    new SortExpression<Charter, string>("airlineCode", x => x.AirlineCode),
                    new SortExpression<Charter, string>("flightNumber", x => x.FlightNumber),
                    new SortExpression<Charter, string>("aircraftCode", x => x.AircraftCode),
                    new SortExpression<Charter, string>("airClassCode", x => x.AirClassCode),
                    new SortExpression<Charter, short?>("stopsCount", x => x.StopsCount),
                    new SortExpression<Charter, string>("timeChangesCode", x => x.TimeChangesCode)
                },
                (Expression<Func<Charter, int>>)(x => x.DepartureCityId));
    }
}