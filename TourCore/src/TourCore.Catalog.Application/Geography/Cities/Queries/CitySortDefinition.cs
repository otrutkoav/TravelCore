using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Cities.Queries
{
    internal static class CitySortDefinition
    {
        public static readonly SortDefinition<City> Instance =
            new SortDefinition<City>(
                new SortExpression<City>[]
                {
                    new SortExpression<City, int>("id", x => x.Id),
                    new SortExpression<City, int>("countryId", x => x.CountryId),
                    new SortExpression<City, int?>("regionId", x => x.RegionId),
                    new SortExpression<City, string>("name", x => x.Name),
                    new SortExpression<City, string>("nameEn", x => x.NameEn),
                    new SortExpression<City, string>("code", x => x.Code),
                    new SortExpression<City, int>("sortOrder", x => x.SortOrder),
                    new SortExpression<City, bool>("isDeparturePoint", x => x.IsDeparturePoint),
                    new SortExpression<City, string>("timeZone", x => x.TimeZone),
                    new SortExpression<City, string>("iataCode", x => x.IataCode),
                    new SortExpression<City, string>("coordinates", x => x.Coordinates)
                },
                (Expression<Func<City, int>>)(x => x.SortOrder));
    }
}