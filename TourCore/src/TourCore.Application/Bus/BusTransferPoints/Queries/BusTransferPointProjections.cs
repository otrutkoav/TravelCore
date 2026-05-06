using System;
using System.Linq.Expressions;
using TourCore.Contracts.Bus.BusTransferPoints;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Bus.BusTransferPoints.Queries
{
    internal static class BusTransferPointProjections
    {
        public static readonly Expression<Func<BusTransferPoint, BusTransferPointListItemDto>> ListItem =
            x => new BusTransferPointListItemDto
            {
                Id = x.Id,
                BusTransferId = x.BusTransferId,
                CountryFromId = x.CountryFromId,
                CityFromId = x.CityFromId,
                CountryToId = x.CountryToId,
                CityToId = x.CityToId,
                TimeFrom = x.TimeFrom,
                TimeTo = x.TimeTo,
                DayFrom = x.DayFrom,
                DayTo = x.DayTo
            };
    }
}