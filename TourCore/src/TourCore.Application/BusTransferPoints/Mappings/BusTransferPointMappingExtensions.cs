using TourCore.Contracts.Bus.BusTransferPoints;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.BusTransferPoints.Mappings
{
    public static class BusTransferPointMappingExtensions
    {
        public static BusTransferPointDto ToDto(this BusTransferPoint entity)
        {
            return new BusTransferPointDto
            {
                Id = entity.Id,
                BusTransferId = entity.BusTransferId,
                CountryFromId = entity.CountryFromId,
                CityFromId = entity.CityFromId,
                CountryToId = entity.CountryToId,
                CityToId = entity.CityToId,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                DayFrom = entity.DayFrom,
                DayTo = entity.DayTo
            };
        }

        public static BusTransferPointListItemDto ToListItemDto(this BusTransferPoint entity)
        {
            return new BusTransferPointListItemDto
            {
                Id = entity.Id,
                BusTransferId = entity.BusTransferId,
                CountryFromId = entity.CountryFromId,
                CityFromId = entity.CityFromId,
                CountryToId = entity.CountryToId,
                CityToId = entity.CityToId,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                DayFrom = entity.DayFrom,
                DayTo = entity.DayTo
            };
        }
    }
}