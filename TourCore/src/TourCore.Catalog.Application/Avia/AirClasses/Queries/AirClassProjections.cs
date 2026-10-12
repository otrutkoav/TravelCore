using System;
using System.Linq.Expressions;
using TourCore.Contracts.Avia.AirClasses;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.AirClasses.Queries
{
    internal static class AirClassProjections
    {
        public static readonly Expression<Func<AirClass, AirClassListItemDto>> ListItem =
            x => new AirClassListItemDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                NameEn = x.NameEn,
                Group = x.Group,
                SortOrder = x.SortOrder
            };
    }
}