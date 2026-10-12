using System;
using System.Linq.Expressions;
using TourCore.Contracts.Seating.SeatingCells;
using TourCore.Domain.Seating.Entities;

using ContractSeatType = TourCore.Contracts.Seating.Enum.SeatType;

namespace TourCore.Application.Seating.SeatingCells.Queries
{
    internal static class SeatingCellProjections
    {
        public static readonly Expression<Func<SeatingCell, SeatingCellListItemDto>> ListItem =
            x => new SeatingCellListItemDto
            {
                Id = x.Id,
                VehiclePlanId = x.VehiclePlanId,
                Number = x.Number,
                Type = x.Type.HasValue
                    ? (ContractSeatType?)(int)x.Type.Value
                    : null,
                SeatsCount = x.SeatsCount,
                Index = x.Index,
                Border = x.Border
            };
    }
}