using TourCore.Contracts.Bus.BusTransfers;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.BusTransfers.Mappings
{
    public static class BusTransferMappingExtensions
    {
        public static BusTransferDto ToDto(this BusTransfer entity)
        {
            return new BusTransferDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CountryFromId = entity.CountryFromId,
                CityFromId = entity.CityFromId,
                CountryToId = entity.CountryToId,
                CityToId = entity.CityToId
            };
        }

        public static BusTransferListItemDto ToListItemDto(this BusTransfer entity)
        {
            return new BusTransferListItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CountryFromId = entity.CountryFromId,
                CityFromId = entity.CityFromId,
                CountryToId = entity.CountryToId,
                CityToId = entity.CityToId
            };
        }
    }
}