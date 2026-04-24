using TourCore.Application.VehiclePlans.DTOs;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.VehiclePlans.Mappings
{
    public static class VehiclePlanMappingExtensions
    {
        public static VehiclePlanDto ToDto(this VehiclePlan entity)
        {
            return new VehiclePlanDto
            {
                Id = entity.Id,
                TransportId = entity.TransportId,
                RowsCount = entity.RowsCount,
                ColumnsCount = entity.ColumnsCount,
                AreaNumber = entity.AreaNumber,
                Name = entity.Name,
                PlanOrientation = entity.PlanOrientation,
                IsAircraft = entity.IsAircraft,
                Dates = entity.Dates,
                Comment = entity.Comment,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static VehiclePlanListItemDto ToListItemDto(this VehiclePlan entity)
        {
            return new VehiclePlanListItemDto
            {
                Id = entity.Id,
                TransportId = entity.TransportId,
                RowsCount = entity.RowsCount,
                ColumnsCount = entity.ColumnsCount,
                AreaNumber = entity.AreaNumber,
                Name = entity.Name,
                IsAircraft = entity.IsAircraft
            };
        }
    }
}