using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.AirClasses.Queries
{
    internal static class AirClassSortDefinition
    {
        public static readonly SortDefinition<AirClass> Instance =
            new SortDefinition<AirClass>(
                new SortExpression<AirClass>[]
                {
                    new SortExpression<AirClass, int>("id", x => x.Id),
                    new SortExpression<AirClass, string>("code", x => x.Code),
                    new SortExpression<AirClass, string>("name", x => x.Name),
                    new SortExpression<AirClass, string>("nameEn", x => x.NameEn),
                    new SortExpression<AirClass, string>("group", x => x.Group),
                    new SortExpression<AirClass, int>("sortOrder", x => x.SortOrder)
                },
                (Expression<Func<AirClass, int>>)(x => x.SortOrder));
    }
}