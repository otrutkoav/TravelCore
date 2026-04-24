using TourCore.Application.TransferDirections.DTOs;
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
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
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