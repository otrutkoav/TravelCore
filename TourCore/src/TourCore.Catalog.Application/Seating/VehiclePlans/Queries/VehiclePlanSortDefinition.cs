using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.Seating.VehiclePlans.Queries
{
    internal static class VehiclePlanSortDefinition
    {
        public static readonly SortDefinition<VehiclePlan> Instance =
            new SortDefinition<VehiclePlan>(
                new SortExpression<VehiclePlan>[]
                {
                    new SortExpression<VehiclePlan, int>("id", x => x.Id),
                    new SortExpression<VehiclePlan, int>("transportId", x => x.TransportId),
                    new SortExpression<VehiclePlan, int>("rowsCount", x => x.RowsCount),
                    new SortExpression<VehiclePlan, int>("columnsCount", x => x.ColumnsCount),
                    new SortExpression<VehiclePlan, int>("areaNumber", x => x.AreaNumber),
                    new SortExpression<VehiclePlan, string>("name", x => x.Name),
                    new SortExpression<VehiclePlan, bool>("planOrientation", x => x.PlanOrientation),
                    new SortExpression<VehiclePlan, bool>("isAircraft", x => x.IsAircraft),
                    new SortExpression<VehiclePlan, string>("dates", x => x.Dates),
                    new SortExpression<VehiclePlan, string>("comment", x => x.Comment)
                },
                (Expression<Func<VehiclePlan, int>>)(x => x.TransportId));
    }
}