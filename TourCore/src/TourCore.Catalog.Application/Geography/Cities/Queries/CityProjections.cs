using System;
using System.Linq.Expressions;
using TourCore.Contracts.Geography.Cities;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Cities.Queries
{
    internal static class CityProjections
    {
        public static readonly Expression<Func<City, CityListItemDto>> ListItem =
            x => new CityListItemDto
            {
                Id = x.Id,
                CountryId = x.CountryId,
                RegionId = x.RegionId,
                Name = x.Name,
                NameEn = x.NameEn,
                Code = x.Code,
                SortOrder = x.SortOrder,
                IsDeparturePoint = x.IsDeparturePoint,
                TimeZone = x.TimeZone,
                IataCode = x.IataCode,
                Coordinates = x.Coordinates
            };
    }
}