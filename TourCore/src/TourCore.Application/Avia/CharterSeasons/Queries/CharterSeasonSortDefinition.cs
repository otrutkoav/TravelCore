using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.CharterSeasons.Queries
{
    internal static class CharterSeasonSortDefinition
    {
        public static readonly SortDefinition<CharterSeason> Instance =
            new SortDefinition<CharterSeason>(
                new SortExpression<CharterSeason>[]
                {
                    new SortExpression<CharterSeason, int>("id", x => x.Id),
                    new SortExpression<CharterSeason, int>("charterId", x => x.CharterId),
                    new SortExpression<CharterSeason, DateTime?>("dateFrom", x => x.DateFrom),
                    new SortExpression<CharterSeason, DateTime?>("dateTo", x => x.DateTo),
                    new SortExpression<CharterSeason, string>("daysOfWeek", x => x.DaysOfWeekValue),
                    new SortExpression<CharterSeason, DateTime?>("timeFrom", x => x.TimeFrom),
                    new SortExpression<CharterSeason, DateTime?>("timeTo", x => x.TimeTo),
                    new SortExpression<CharterSeason, bool>("isNextDayArrival", x => x.IsNextDayArrival),
                    new SortExpression<CharterSeason, string>("remark", x => x.Remark)
                },
                (Expression<Func<CharterSeason, int>>)(x => x.CharterId));
    }
}