using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.Airports.Queries
{
    internal static class AirportSortDefinition
    {
        public static readonly SortDefinition<Airport> Instance =
            new SortDefinition<Airport>(
                new SortExpression<Airport>[]
                {
                    new SortExpression<Airport, int>("id", x => x.Id),
                    new SortExpression<Airport, int>("cityId", x => x.CityId),
                    new SortExpression<Airport, string>("code", x => x.Code),
                    new SortExpression<Airport, string>("name", x => x.Name),
                    new SortExpression<Airport, string>("nameEn", x => x.NameEn),
                    new SortExpression<Airport, string>("icaoCode", x => x.IcaoCode),
                    new SortExpression<Airport, string>("letterCode", x => x.LetterCode)
                },
                (Expression<Func<Airport, string>>)(x => x.Name));
    }
}