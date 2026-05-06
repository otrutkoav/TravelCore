using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Transfers.TransferDirections.Queries
{
    internal static class TransferDirectionSortDefinition
    {
        public static readonly SortDefinition<TransferDirection> Instance =
            new SortDefinition<TransferDirection>(
                new SortExpression<TransferDirection>[]
                {
                    new SortExpression<TransferDirection, int>("id", x => x.Id),
                    new SortExpression<TransferDirection, string>("name", x => x.Name)
                },
                (Expression<Func<TransferDirection, string>>)(x => x.Name));
    }
}