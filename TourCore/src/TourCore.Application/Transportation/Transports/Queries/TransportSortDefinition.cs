using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Application.Transportation.Transports.Queries
{
    internal static class TransportSortDefinition
    {
        public static readonly SortDefinition<Transport> Instance =
            new SortDefinition<Transport>(
                new SortExpression<Transport>[]
                {
                    new SortExpression<Transport, int>("id", x => x.Id),
                    new SortExpression<Transport, string>("name", x => x.Name),
                    new SortExpression<Transport, string>("nameEn", x => x.NameEn),
                    new SortExpression<Transport, short?>("seatsCount", x => x.SeatsCount),
                    new SortExpression<Transport, int>("showOrder", x => x.ShowOrder)
                },
                (Expression<Func<Transport, int>>)(x => x.ShowOrder));
    }
}