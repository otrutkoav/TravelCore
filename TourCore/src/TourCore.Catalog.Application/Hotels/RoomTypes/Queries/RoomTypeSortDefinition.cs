using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.RoomTypes.Queries
{
    internal static class RoomTypeSortDefinition
    {
        public static readonly SortDefinition<RoomType> Instance =
            new SortDefinition<RoomType>(
                new SortExpression<RoomType>[]
                {
                    new SortExpression<RoomType, int>("id", x => x.Id),
                    new SortExpression<RoomType, string>("code", x => x.Code),
                    new SortExpression<RoomType, string>("name", x => x.Name),
                    new SortExpression<RoomType, string>("nameEn", x => x.NameEn),
                    new SortExpression<RoomType, short?>("places", x => x.Places),
                    new SortExpression<RoomType, short?>("extraPlaces", x => x.ExtraPlaces),
                    new SortExpression<RoomType, int>("sortOrder", x => x.SortOrder),
                    new SortExpression<RoomType, string>("description", x => x.Description)
                },
                (Expression<Func<RoomType, int>>)(x => x.SortOrder));
    }
}