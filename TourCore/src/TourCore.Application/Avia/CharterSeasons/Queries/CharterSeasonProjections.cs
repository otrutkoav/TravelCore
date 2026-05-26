using System;
using System.Linq.Expressions;
using TourCore.Contracts.Avia.CharterSeasons;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.CharterSeasons.Queries
{
    internal static class CharterSeasonProjections
    {
        public static readonly Expression<Func<CharterSeason, CharterSeasonListItemDto>> ListItem =
            x => new CharterSeasonListItemDto
            {
                Id = x.Id,
                CharterId = x.CharterId,
                DateFrom = x.DateFrom,
                DateTo = x.DateTo,
                DaysOfWeek = x.DaysOfWeekValue,
                TimeFrom = x.TimeFrom,
                TimeTo = x.TimeTo,
                IsNextDayArrival = x.IsNextDayArrival,
                Remark = x.Remark
            };
    }
}