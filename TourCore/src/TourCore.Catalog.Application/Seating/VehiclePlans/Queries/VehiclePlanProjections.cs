using System;
using System.Linq.Expressions;
using TourCore.Contracts.Seating.VehiclePlans;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.Seating.VehiclePlans.Queries
{
    internal static class VehiclePlanProjections
    {
        public static readonly Expression<Func<VehiclePlan, VehiclePlanListItemDto>> ListItem =
            x => new VehiclePlanListItemDto
            {
                Id = x.Id,
                TransportId = x.TransportId,
                RowsCount = x.RowsCount,
                ColumnsCount = x.ColumnsCount,
                AreaNumber = x.AreaNumber,
                Name = x.Name,
                PlanOrientation = x.PlanOrientation,
                IsAircraft = x.IsAircraft,
                Dates = x.Dates,
                Comment = x.Comment
            };
    }
}