using System;
using System.Linq.Expressions;
using TourCore.Contracts.Hotels.MealTypes;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.MealTypes.Queries
{
    internal static class MealTypeProjections
    {
        public static readonly Expression<Func<MealType, MealTypeListItemDto>> ListItem =
            x => new MealTypeListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                NameEn = x.NameEn,
                Code = x.Code,
                GlobalCode = x.GlobalCode,
                SortOrder = x.SortOrder,
                Description = x.Description
            };
    }
}