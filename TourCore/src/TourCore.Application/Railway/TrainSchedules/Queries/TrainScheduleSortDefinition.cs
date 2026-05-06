using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.Railway.TrainSchedules.Queries
{
    internal static class TrainScheduleSortDefinition
    {
        public static readonly SortDefinition<TrainSchedule> Instance =
            new SortDefinition<TrainSchedule>(
                new SortExpression<TrainSchedule>[]
                {
                    new SortExpression<TrainSchedule, int>("id", x => x.Id),
                    new SortExpression<TrainSchedule, int>("railwayTransferId", x => x.RailwayTransferId),
                    new SortExpression<TrainSchedule, DateTime?>("dateFrom", x => x.DateFrom),
                    new SortExpression<TrainSchedule, DateTime?>("dateTo", x => x.DateTo),
                    new SortExpression<TrainSchedule, DateTime?>("timeFrom", x => x.TimeFrom),
                    new SortExpression<TrainSchedule, DateTime?>("timeTo", x => x.TimeTo),
                    new SortExpression<TrainSchedule, short?>("daysOnRoad", x => x.DaysOnRoad),
                    new SortExpression<TrainSchedule, string>("remark", x => x.Remark)
                },
                (Expression<Func<TrainSchedule, int>>)(x => x.RailwayTransferId));
    }
}