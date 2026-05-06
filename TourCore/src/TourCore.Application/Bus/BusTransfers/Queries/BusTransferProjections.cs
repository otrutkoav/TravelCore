using System;
using System.Linq.Expressions;
using TourCore.Contracts.Bus.BusTransfers;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Bus.BusTransfers.Queries
{
    internal static class BusTransferProjections
    {
        public static readonly Expression<Func<BusTransfer, BusTransferListItemDto>> ListItem =
            x => new BusTransferListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                CountryFromId = x.CountryFromId,
                CityFromId = x.CityFromId,
                CountryToId = x.CountryToId,
                CityToId = x.CityToId
            };
    }
}