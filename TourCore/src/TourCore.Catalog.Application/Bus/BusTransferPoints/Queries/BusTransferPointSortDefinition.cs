using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Bus.BusTransferPoints.Queries
{
    internal static class BusTransferPointSortDefinition
    {
        public static readonly SortDefinition<BusTransferPoint> Instance =
            new SortDefinition<BusTransferPoint>(
                new SortExpression<BusTransferPoint>[]
                {
                    new SortExpression<BusTransferPoint, int>("id", x => x.Id),
                    new SortExpression<BusTransferPoint, int>("busTransferId", x => x.BusTransferId),
                    new SortExpression<BusTransferPoint, int>("countryFromId", x => x.CountryFromId),
                    new SortExpression<BusTransferPoint, int>("cityFromId", x => x.CityFromId),
                    new SortExpression<BusTransferPoint, int>("countryToId", x => x.CountryToId),
                    new SortExpression<BusTransferPoint, int>("cityToId", x => x.CityToId),
                    new SortExpression<BusTransferPoint, DateTime?>("timeFrom", x => x.TimeFrom),
                    new SortExpression<BusTransferPoint, DateTime?>("timeTo", x => x.TimeTo),
                    new SortExpression<BusTransferPoint, short?>("dayFrom", x => x.DayFrom),
                    new SortExpression<BusTransferPoint, short?>("dayTo", x => x.DayTo)
                },
                (Expression<Func<BusTransferPoint, int>>)(x => x.BusTransferId));
    }
}