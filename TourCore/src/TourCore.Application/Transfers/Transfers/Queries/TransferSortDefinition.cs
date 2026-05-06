using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Transfers.Transfers.Queries
{
    internal static class TransferSortDefinition
    {
        public static readonly SortDefinition<Transfer> Instance =
            new SortDefinition<Transfer>(
                new SortExpression<Transfer>[]
                {
                    new SortExpression<Transfer, int>("id", x => x.Id),
                    new SortExpression<Transfer, string>("name", x => x.Name),
                    new SortExpression<Transfer, string>("nameEn", x => x.NameEn),
                    new SortExpression<Transfer, DateTime?>("timeFrom", x => x.TimeFrom),
                    new SortExpression<Transfer, DateTime?>("timeTo", x => x.TimeTo),
                    new SortExpression<Transfer, string>("durationText", x => x.DurationText),
                    new SortExpression<Transfer, string>("placeFrom", x => x.PlaceFrom),
                    new SortExpression<Transfer, string>("placeTo", x => x.PlaceTo),
                    new SortExpression<Transfer, bool>("isMain", x => x.IsMain),
                    new SortExpression<Transfer, int?>("cityId", x => x.CityId),
                    new SortExpression<Transfer, int?>("directionId", x => x.DirectionId),
                    new SortExpression<Transfer, int>("showOrder", x => x.ShowOrder),
                    new SortExpression<Transfer, bool>("autoApplyFrom", x => x.AutoApplyFrom),
                    new SortExpression<Transfer, bool>("autoApplyTo", x => x.AutoApplyTo)
                },
                (Expression<Func<Transfer, int>>)(x => x.ShowOrder));
    }
}