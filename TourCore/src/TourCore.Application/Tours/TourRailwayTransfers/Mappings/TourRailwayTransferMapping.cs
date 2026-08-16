using System;
using System.Linq;
using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tours
{
    internal static class TourRailwayTransferMapping
    {
        public static TourRailwayTransferDto ToDto(this TourRailwayTransfer x) =>
            new TourRailwayTransferDto
            {
                Id = x.Id,
                DepartureCountryIds = x
                    .DepartureCountries.Select(i => i.CountryId)
                    .OrderBy(i => i)
                    .ToArray(),
                DepartureCityIds = x
                    .DepartureCities.Select(i => i.CityId)
                    .OrderBy(i => i)
                    .ToArray(),
                ArrivalCountryIds = x
                    .ArrivalCountries.Select(i => i.CountryId)
                    .OrderBy(i => i)
                    .ToArray(),
                ArrivalCityIds = x.ArrivalCities.Select(i => i.CityId).OrderBy(i => i).ToArray(),
                RailwayTransferIds = x
                    .Options.Select(i => i.RailwayTransferId)
                    .OrderBy(i => i)
                    .ToArray(),
                TransportIds = x.Transports.Select(i => i.TransportId).OrderBy(i => i).ToArray(),
                DepartureTimeFrom = x.DepartureTimeFrom,
                DepartureTimeTo = x.DepartureTimeTo,
                ArrivalTimeFrom = x.ArrivalTimeFrom,
                ArrivalTimeTo = x.ArrivalTimeTo,
            };

        public static TourRailwayTransferDto Empty() =>
            new TourRailwayTransferDto
            {
                DepartureCountryIds = new int[0],
                DepartureCityIds = new int[0],
                ArrivalCountryIds = new int[0],
                ArrivalCityIds = new int[0],
                RailwayTransferIds = new int[0],
                TransportIds = new int[0],
                DepartureTimeFrom = TimeSpan.Zero,
                DepartureTimeTo = new TimeSpan(23, 59, 59),
                ArrivalTimeFrom = TimeSpan.Zero,
                ArrivalTimeTo = new TimeSpan(23, 59, 59),
            };
    }
}
