using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.RoomCategories.Queries
{
    internal static class RoomCategorySortDefinition
    {
        public static readonly SortDefinition<RoomCategory> Instance =
            new SortDefinition<RoomCategory>(
                new SortExpression<RoomCategory>[]
                {
                    new SortExpression<RoomCategory, int>("id", x => x.Id),
                    new SortExpression<RoomCategory, string>("code", x => x.Code),
                    new SortExpression<RoomCategory, string>("name", x => x.Name),
                    new SortExpression<RoomCategory, string>("nameEn", x => x.NameEn),
                    new SortExpression<RoomCategory, int>("sortOrder", x => x.SortOrder),
                    new SortExpression<RoomCategory, string>("description", x => x.Description)
                },
                (Expression<Func<RoomCategory, int>>)(x => x.SortOrder));
    }
}