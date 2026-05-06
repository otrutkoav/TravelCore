using System;
using System.Linq.Expressions;
using TourCore.Contracts.Finance.Rates;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.Finance.Rates.Queries
{
    internal static class RateProjections
    {
        public static readonly Expression<Func<Rate, RateListItemDto>> ListItem =
            x => new RateListItemDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                IsoCode = x.IsoCode,
                IsMain = x.IsMain,
                IsNational = x.IsNational,
                ShowInSearch = x.ShowInSearch
            };
    }
}