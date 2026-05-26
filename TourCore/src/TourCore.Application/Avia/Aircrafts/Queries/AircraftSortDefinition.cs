using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.Aircrafts.Queries
{
    internal static class AircraftSortDefinition
    {
        public static readonly SortDefinition<Aircraft> Instance =
            new SortDefinition<Aircraft>(
                new SortExpression<Aircraft>[]
                {
                    new SortExpression<Aircraft, int>("id", x => x.Id),
                    new SortExpression<Aircraft, string>("code", x => x.Code),
                    new SortExpression<Aircraft, string>("name", x => x.Name),
                    new SortExpression<Aircraft, string>("nameEn", x => x.NameEn)
                },
                (Expression<Func<Aircraft, string>>)(x => x.Name));
    }
}