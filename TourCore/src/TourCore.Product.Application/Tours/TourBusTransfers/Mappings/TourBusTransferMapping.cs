using System;
using System.Linq;
using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tours
{
    internal static class TourBusTransferMapping
    {
        public static TourBusTransferDto ToDto(this TourBusTransfer transfer)
        {
            return new TourBusTransferDto
            {
                Id = transfer.Id,
                DepartureCountryIds = transfer
                    .DepartureCountries.Select(x => x.CountryId)
                    .OrderBy(x => x)
                    .ToArray(),
                DepartureCityIds = transfer
                    .DepartureCities.Select(x => x.CityId)
                    .OrderBy(x => x)
                    .ToArray(),
                ArrivalCountryIds = transfer
                    .ArrivalCountries.Select(x => x.CountryId)
                    .OrderBy(x => x)
                    .ToArray(),
                ArrivalCityIds = transfer
                    .ArrivalCities.Select(x => x.CityId)
                    .OrderBy(x => x)
                    .ToArray(),
                BusTransferIds = transfer
                    .Options.Select(x => x.BusTransferId)
                    .OrderBy(x => x)
                    .ToArray(),
                BoardingPointIds = transfer
                    .BoardingPoints.Select(x => x.BoardingPointId)
                    .OrderBy(x => x)
                    .ToArray(),
                TransportIds = transfer
                    .Transports.Select(x => x.TransportId)
                    .OrderBy(x => x)
                    .ToArray(),
                DepartureTimeFrom = transfer.DepartureTimeFrom,
                DepartureTimeTo = transfer.DepartureTimeTo,
                ArrivalTimeFrom = transfer.ArrivalTimeFrom,
                ArrivalTimeTo = transfer.ArrivalTimeTo,
            };
        }

        public static TourBusTransferDto Empty()
        {
            return new TourBusTransferDto
            {
                DepartureCountryIds = Array.Empty<int>(),
                DepartureCityIds = Array.Empty<int>(),
                ArrivalCountryIds = Array.Empty<int>(),
                ArrivalCityIds = Array.Empty<int>(),
                BusTransferIds = Array.Empty<int>(),
                BoardingPointIds = Array.Empty<int>(),
                TransportIds = Array.Empty<int>(),
                DepartureTimeFrom = TimeSpan.Zero,
                DepartureTimeTo = new TimeSpan(23, 59, 59),
                ArrivalTimeFrom = TimeSpan.Zero,
                ArrivalTimeTo = new TimeSpan(23, 59, 59),
            };
        }
    }
}
