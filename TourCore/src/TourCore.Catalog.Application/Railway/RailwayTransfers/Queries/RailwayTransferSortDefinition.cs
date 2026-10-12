using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.Railway.RailwayTransfers.Queries
{
    internal static class RailwayTransferSortDefinition
    {
        public static readonly SortDefinition<RailwayTransfer> Instance =
            new SortDefinition<RailwayTransfer>(
                new SortExpression<RailwayTransfer>[]
                {
                    new SortExpression<RailwayTransfer, int>("id", x => x.Id),
                    new SortExpression<RailwayTransfer, string>("name", x => x.Name),
                    new SortExpression<RailwayTransfer, int>("countryFromId", x => x.CountryFromId),
                    new SortExpression<RailwayTransfer, int>("cityFromId", x => x.CityFromId),
                    new SortExpression<RailwayTransfer, int>("countryToId", x => x.CountryToId),
                    new SortExpression<RailwayTransfer, int>("cityToId", x => x.CityToId)
                },
                (Expression<Func<RailwayTransfer, int>>)(x => x.CountryFromId));
    }
}