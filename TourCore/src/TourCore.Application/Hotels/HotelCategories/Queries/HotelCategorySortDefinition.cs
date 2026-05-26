using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.HotelCategories.Queries
{
    internal static class HotelCategorySortDefinition
    {
        public static readonly SortDefinition<HotelCategory> Instance =
            new SortDefinition<HotelCategory>(
                new SortExpression<HotelCategory>[]
                {
                    new SortExpression<HotelCategory, int>("id", x => x.Id),
                    new SortExpression<HotelCategory, string>("name", x => x.Name),
                    new SortExpression<HotelCategory, string>("nameEn", x => x.NameEn),
                    new SortExpression<HotelCategory, int?>("printOrder", x => x.PrintOrder),
                    new SortExpression<HotelCategory, string>("globalCode", x => x.GlobalCode),
                    new SortExpression<HotelCategory, string>("description", x => x.Description)
                },
                (Expression<Func<HotelCategory, int?>>)(x => x.PrintOrder));
    }
}