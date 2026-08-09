using System;
using System.Collections.Generic;
using System.Linq;
using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tours
{
    internal static class TourFlightMapping
    {
        public static string Pack(IEnumerable<int> values)
        {
            return string.Join(
                ",",
                (values ?? Enumerable.Empty<int>()).Where(x => x > 0).Distinct().OrderBy(x => x)
            );
        }

        public static IReadOnlyCollection<int> Unpack(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? Array.Empty<int>()
                : value.Split(',').Select(int.Parse).ToArray();
        }

        public static TourFlightDto ToDto(this TourFlight flight)
        {
            return new TourFlightDto
            {
                Id = flight.Id,
                OrderIndex = flight.OrderIndex,
                DepartureCountryIds = Unpack(flight.DepartureCountryIds),
                DepartureCityIds = Unpack(flight.DepartureCityIds),
                ArrivalCountryIds = Unpack(flight.ArrivalCountryIds),
                ArrivalCityIds = Unpack(flight.ArrivalCityIds),
                AirlineIds = Unpack(flight.AirlineIds),
                FlightIds = Unpack(flight.FlightIds),
                DepartureAirportIds = Unpack(flight.DepartureAirportIds),
                ArrivalAirportIds = Unpack(flight.ArrivalAirportIds),
                FareIds = Unpack(flight.FareIds),
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
