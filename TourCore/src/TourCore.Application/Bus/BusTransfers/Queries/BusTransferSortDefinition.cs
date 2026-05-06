using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Bus.BusTransfers.Queries
{
    internal static class BusTransferSortDefinition
    {
        public static readonly SortDefinition<BusTransfer> Instance =
            new SortDefinition<BusTransfer>(
                new SortExpression<BusTransfer>[]
                {
                    new SortExpression<BusTransfer, int>("id", x => x.Id),
                    new SortExpression<BusTransfer, string>("name", x => x.Name),
                    new SortExpression<BusTransfer, int>("countryFromId", x => x.CountryFromId),
                    new SortExpression<BusTransfer, int>("cityFromId", x => x.CityFromId),
                    new SortExpression<BusTransfer, int>("countryToId", x => x.CountryToId),
                    new SortExpression<BusTransfer, int>("cityToId", x => x.CityToId)
                },
                (Expression<Func<BusTransfer, int>>)(x => x.CountryFromId));
    }
}