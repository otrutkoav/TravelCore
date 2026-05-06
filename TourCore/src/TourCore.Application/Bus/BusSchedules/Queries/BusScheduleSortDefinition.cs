using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Bus.BusSchedules.Queries
{
    internal static class BusScheduleSortDefinition
    {
        public static readonly SortDefinition<BusSchedule> Instance =
            new SortDefinition<BusSchedule>(
                new SortExpression<BusSchedule>[]
                {
                    new SortExpression<BusSchedule, int>("id", x => x.Id),
                    new SortExpression<BusSchedule, int>("busTransferId", x => x.BusTransferId),
                    new SortExpression<BusSchedule, DateTime?>("dateFrom", x => x.DateFrom),
                    new SortExpression<BusSchedule, DateTime?>("dateTo", x => x.DateTo),
                    new SortExpression<BusSchedule, DateTime?>("timeFrom", x => x.TimeFrom),
                    new SortExpression<BusSchedule, DateTime?>("timeTo", x => x.TimeTo),
                    new SortExpression<BusSchedule, short?>("daysOnRoad", x => x.DaysOnRoad)
                },
                (Expression<Func<BusSchedule, int>>)(x => x.BusTransferId));
    }
}