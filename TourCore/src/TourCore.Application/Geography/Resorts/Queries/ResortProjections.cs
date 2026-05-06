using System;
using System.Linq.Expressions;
using TourCore.Contracts.Geography.Resorts;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Resorts.Queries
{
    internal static class ResortProjections
    {
        public static readonly Expression<Func<Resort, ResortListItemDto>> ListItem =
            x => new ResortListItemDto
            {
                Id = x.Id,
                CountryId = x.CountryId,
                Name = x.Name,
                NameEn = x.NameEn
            };
    }
}