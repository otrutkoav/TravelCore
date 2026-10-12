using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Resorts.Queries
{
    internal static class ResortSortDefinition
    {
        public static readonly SortDefinition<Resort> Instance =
            new SortDefinition<Resort>(
                new SortExpression<Resort>[]
                {
                    new SortExpression<Resort, int>("id", x => x.Id),
                    new SortExpression<Resort, int>("countryId", x => x.CountryId),
                    new SortExpression<Resort, string>("name", x => x.Name),
                    new SortExpression<Resort, string>("nameEn", x => x.NameEn)
                },
                (Expression<Func<Resort, string>>)(x => x.Name));
    }
}