using TourCore.Application.SeatingCells.DTOs;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.SeatingCells.Mappings
{
    public static class SeatingCellMappingExtensions
    {
        public static SeatingCellDto ToDto(this SeatingCell entity)
        {
            return new SeatingCellDto
            {
                Id = entity.Id,
                VehiclePlanId = entity.VehiclePlanId,
                Number = entity.Number,
                Type = entity.Type,
                SeatsCount = entity.SeatsCount,
                Index = entity.Index,
                Border = entity.Border,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static SeatingCellListItemDto ToListItemDto(this SeatingCell entity)
        {
            return new SeatingCellListItemDto
            {
                Id = entity.Id,
                VehiclePlanId = entity.VehiclePlanId,
                Number = entity.Number,
                Type = entity.Type,
                Index = entity.Index
            };
        }
    }
}