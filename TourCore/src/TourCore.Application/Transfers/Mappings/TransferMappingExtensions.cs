using TourCore.Contracts.Transfers.Transfers;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Transfers.Mappings
{
    public static class TransferMappingExtensions
    {
        public static TransferDto ToDto(this Transfer entity)
        {
            return new TransferDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                DurationText = entity.DurationText,
                PlaceFrom = entity.PlaceFrom,
                PlaceTo = entity.PlaceTo,
                IsMain = entity.IsMain,
                CityId = entity.CityId,
                DirectionId = entity.DirectionId,
                Url = entity.Url,
                ShowOrder = entity.ShowOrder,
                AutoApplyFrom = entity.AutoApplyFrom,
                AutoApplyTo = entity.AutoApplyTo
            };
        }

        public static TransferListItemDto ToListItemDto(this Transfer entity)
        {
            return new TransferListItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                DurationText = entity.DurationText,
                PlaceFrom = entity.PlaceFrom,
                PlaceTo = entity.PlaceTo,
                IsMain = entity.IsMain,
                CityId = entity.CityId,
                DirectionId = entity.DirectionId,
                ShowOrder = entity.ShowOrder,
                AutoApplyFrom = entity.AutoApplyFrom,
                AutoApplyTo = entity.AutoApplyTo
            };
        }
    }
}