using System;
using System.Linq.Expressions;
using TourCore.Contracts.Hotels.RoomCategories;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.RoomCategories.Queries
{
    internal static class RoomCategoryProjections
    {
        public static readonly Expression<Func<RoomCategory, RoomCategoryListItemDto>> ListItem =
            x => new RoomCategoryListItemDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                NameEn = x.NameEn,
                SortOrder = x.SortOrder,
                Description = x.Description
            };
    }
}