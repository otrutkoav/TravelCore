using System;
using System.Linq.Expressions;
using TourCore.Contracts.Railway.RailwayTransfers;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.Railway.RailwayTransfers.Queries
{
    internal static class RailwayTransferProjections
    {
        public static readonly Expression<Func<RailwayTransfer, RailwayTransferListItemDto>> ListItem =
            x => new RailwayTransferListItemDto
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