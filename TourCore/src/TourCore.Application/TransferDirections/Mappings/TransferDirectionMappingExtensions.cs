using TourCore.Contracts.Transfers.TransferDirections;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.TransferDirections.Mappings
{
    public static class TransferDirectionMappingExtensions
    {
        public static TransferDirectionDto ToDto(this TransferDirection entity)
        {
            return new TransferDirectionDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static TransferDirectionListItemDto ToListItemDto(this TransferDirection entity)
        {
            return new TransferDirectionListItemDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}