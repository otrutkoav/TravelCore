using System;
using System.Linq.Expressions;
using TourCore.Contracts.Avia.Charters;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.Charters.Queries
{
    internal static class CharterProjections
    {
        public static readonly Expression<Func<Charter, CharterListItemDto>> ListItem =
            x => new CharterListItemDto
            {
                Id = x.Id,
                DepartureCityId = x.DepartureCityId,
                DepartureAirportCode = x.DepartureAirportCode,
                ArrivalCityId = x.ArrivalCityId,
                ArrivalAirportCode = x.ArrivalAirportCode,
                AirlineCode = x.AirlineCode,
                FlightNumber = x.FlightNumber,
                AircraftCode = x.AircraftCode,
                AirClassCode = x.AirClassCode,
                StopsCount = x.StopsCount,
                TimeChangesCode = x.TimeChangesCode
            };
    }
}