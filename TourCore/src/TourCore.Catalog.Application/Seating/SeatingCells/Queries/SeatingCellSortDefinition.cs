using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.Seating.SeatingCells.Queries
{
    internal static class SeatingCellSortDefinition
    {
        public static readonly SortDefinition<SeatingCell> Instance =
            new SortDefinition<SeatingCell>(
                new SortExpression<SeatingCell>[]
                {
                    new SortExpression<SeatingCell, int>("id", x => x.Id),
                    new SortExpression<SeatingCell, int>("vehiclePlanId", x => x.VehiclePlanId),
                    new SortExpression<SeatingCell, string>("number", x => x.Number),
                    new SortExpression<SeatingCell, SeatType?>("type", x => x.Type),
                    new SortExpression<SeatingCell, short?>("seatsCount", x => x.SeatsCount),
                    new SortExpression<SeatingCell, int>("index", x => x.Index),
                    new SortExpression<SeatingCell, string>("border", x => x.Border)
                },
                (Expression<Func<SeatingCell, int>>)(x => x.VehiclePlanId));
    }
}