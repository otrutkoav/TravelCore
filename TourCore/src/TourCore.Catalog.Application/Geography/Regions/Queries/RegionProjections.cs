using System;
using System.Linq.Expressions;
using TourCore.Contracts.Geography.Regions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Regions.Queries
{
    internal static class RegionProjections
    {
        public static readonly Expression<Func<Region, RegionListItemDto>> ListItem =
            x => new RegionListItemDto
            {
                Id = x.Id,
                CountryId = x.CountryId,
                Name = x.Name,
                NameEn = x.NameEn,
                Code = x.Code,
                SortOrder = x.SortOrder
            };
    }
}