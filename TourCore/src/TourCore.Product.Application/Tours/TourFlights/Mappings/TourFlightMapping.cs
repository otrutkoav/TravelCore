using System.Collections.Generic;
using System.Linq;
using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tours
{
    internal static class TourFlightMapping
    {
        private static IReadOnlyCollection<int> GetIds(
            TourFlight flight,
            TourFlightRestrictionType type
        )
        {
            return flight
                .Restrictions.Where(x => x.Type == type)
                .Select(x => x.ReferenceId)
                .OrderBy(x => x)
                .ToArray();
        }

        public static TourFlightDto ToDto(this TourFlight flight)
        {
            return new TourFlightDto
            {
                Id = flight.Id,
                OrderIndex = flight.OrderIndex,
                DepartureCountryIds = GetIds(flight, TourFlightRestrictionType.DepartureCountry),
                DepartureCityIds = GetIds(flight, TourFlightRestrictionType.DepartureCity),
                ArrivalCountryIds = GetIds(flight, TourFlightRestrictionType.ArrivalCountry),
                ArrivalCityIds = GetIds(flight, TourFlightRestrictionType.ArrivalCity),
                AirlineIds = GetIds(flight, TourFlightRestrictionType.Airline),
                FlightIds = GetIds(flight, TourFlightRestrictionType.Flight),
                DepartureAirportIds = GetIds(flight, TourFlightRestrictionType.DepartureAirport),
                ArrivalAirportIds = GetIds(flight, TourFlightRestrictionType.ArrivalAirport),
                FareIds = GetIds(flight, TourFlightRestrictionType.Fare),
                MinConnectionMinutes = flight.MinConnectionMinutes,
                MaxConnectionMinutes = flight.MaxConnectionMinutes,
            };
        }

        public static TourFlightGroupDto ToDto(this TourFlightGroup group)
        {
            return new TourFlightGroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Priority = group.Priority,
                OrderIndex = group.OrderIndex,
                Flights = group.Flights.OrderBy(x => x.OrderIndex).Select(ToDto).ToArray(),
            };
        }
    }
}
