using TourCore.Application.BusTransfers.DTOs;
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
                CityToId = entity.CityToId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
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