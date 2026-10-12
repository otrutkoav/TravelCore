using System;
using System.Linq.Expressions;
using TourCore.Contracts.Hotels.HotelCategories;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.HotelCategories.Queries
{
    internal static class HotelCategoryProjections
    {
        public static readonly Expression<Func<HotelCategory, HotelCategoryListItemDto>> ListItem =
            x => new HotelCategoryListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                NameEn = x.NameEn,
                PrintOrder = x.PrintOrder,
                GlobalCode = x.GlobalCode,
                Description = x.Description
            };
    }
}