using System;
using System.Linq.Expressions;
using TourCore.Contracts.Transfers.Transfers;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Transfers.Transfers.Queries
{
    internal static class TransferProjections
    {
        public static readonly Expression<Func<Transfer, TransferListItemDto>> ListItem =
            x => new TransferListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                NameEn = x.NameEn,
                TimeFrom = x.TimeFrom,
                TimeTo = x.TimeTo,
                DurationText = x.DurationText,
                PlaceFrom = x.PlaceFrom,
                PlaceTo = x.PlaceTo,
                IsMain = x.IsMain,
                CityId = x.CityId,
                DirectionId = x.DirectionId,
                ShowOrder = x.ShowOrder,
                AutoApplyFrom = x.AutoApplyFrom,
                AutoApplyTo = x.AutoApplyTo
            };
    }
}