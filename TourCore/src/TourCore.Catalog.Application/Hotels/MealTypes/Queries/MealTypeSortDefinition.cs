using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.MealTypes.Queries
{
    internal static class MealTypeSortDefinition
    {
        public static readonly SortDefinition<MealType> Instance =
            new SortDefinition<MealType>(
                new SortExpression<MealType>[]
                {
                    new SortExpression<MealType, int>("id", x => x.Id),
                    new SortExpression<MealType, string>("name", x => x.Name),
                    new SortExpression<MealType, string>("nameEn", x => x.NameEn),
                    new SortExpression<MealType, string>("code", x => x.Code),
                    new SortExpression<MealType, string>("globalCode", x => x.GlobalCode),
                    new SortExpression<MealType, int>("sortOrder", x => x.SortOrder),
                    new SortExpression<MealType, string>("description", x => x.Description)
                },
                (Expression<Func<MealType, int>>)(x => x.SortOrder));
    }
}