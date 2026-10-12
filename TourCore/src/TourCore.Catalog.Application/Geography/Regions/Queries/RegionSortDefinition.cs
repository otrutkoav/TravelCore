using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Regions.Queries
{
    internal static class RegionSortDefinition
    {
        public static readonly SortDefinition<Region> Instance =
            new SortDefinition<Region>(
                new SortExpression<Region>[]
                {
                    new SortExpression<Region, int>("id", x => x.Id),
                    new SortExpression<Region, int>("countryId", x => x.CountryId),
                    new SortExpression<Region, string>("name", x => x.Name),
                    new SortExpression<Region, string>("nameEn", x => x.NameEn),
                    new SortExpression<Region, string>("code", x => x.Code),
                    new SortExpression<Region, int>("sortOrder", x => x.SortOrder)
                },
                (Expression<Func<Region, int>>)(x => x.SortOrder));
    }
}