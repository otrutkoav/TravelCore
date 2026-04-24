using TourCore.Contracts.Railway.RailwayTransfers;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.RailwayTransfers.Mappings
{
    public static class RailwayTransferMappingExtensions
    {
        public static RailwayTransferDto ToDto(this RailwayTransfer entity)
        {
            return new RailwayTransferDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CountryFromId = entity.CountryFromId,
                CityFromId = entity.CityFromId,
                CountryToId = entity.CountryToId,
                CityToId = entity.CityToId
            };
        }

        public static RailwayTransferListItemDto ToListItemDto(this RailwayTransfer entity)
        {
            return new RailwayTransferListItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CityFromId = entity.CityFromId,
                CityToId = entity.CityToId
            };
        }
    }
}