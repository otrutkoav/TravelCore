using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.Finance.Rates.Queries
{
    internal static class RateSortDefinition
    {
        public static readonly SortDefinition<Rate> Instance =
            new SortDefinition<Rate>(
                new SortExpression<Rate>[]
                {
                    new SortExpression<Rate, int>("id", x => x.Id),
                    new SortExpression<Rate, string>("code", x => x.Code),
                    new SortExpression<Rate, string>("name", x => x.Name),
                    new SortExpression<Rate, string>("isoCode", x => x.IsoCode),
                    new SortExpression<Rate, bool>("isMain", x => x.IsMain),
                    new SortExpression<Rate, bool>("isNational", x => x.IsNational),
                    new SortExpression<Rate, bool>("showInSearch", x => x.ShowInSearch)
                },
                (Expression<Func<Rate, string>>)(x => x.Name));
    }
}