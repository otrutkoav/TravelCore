using System;
using System.Linq.Expressions;
using TourCore.Contracts.Hotels.RoomTypes;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.RoomTypes.Queries
{
    internal static class RoomTypeProjections
    {
        public static readonly Expression<Func<RoomType, RoomTypeListItemDto>> ListItem =
            x => new RoomTypeListItemDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                NameEn = x.NameEn,
                Places = x.Places,
                ExtraPlaces = x.ExtraPlaces,
                SortOrder = x.SortOrder,
                Description = x.Description
            };
    }
}