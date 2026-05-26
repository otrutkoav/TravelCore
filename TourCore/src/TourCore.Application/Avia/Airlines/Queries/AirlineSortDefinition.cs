using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.Airlines.Queries
{
    internal static class AirlineSortDefinition
    {
        public static readonly SortDefinition<Airline> Instance =
            new SortDefinition<Airline>(
                new SortExpression<Airline>[]
                {
                    new SortExpression<Airline, int>("id", x => x.Id),
                    new SortExpression<Airline, string>("code", x => x.Code),
                    new SortExpression<Airline, string>("name", x => x.Name),
                    new SortExpression<Airline, string>("nameEn", x => x.NameEn),
                    new SortExpression<Airline, string>("icaoCode", x => x.IcaoCode)
                },
                (Expression<Func<Airline, string>>)(x => x.Name));
    }
}