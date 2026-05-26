using System;
using System.Linq.Expressions;
using TourCore.Contracts.Hotels.AccommodationTypes;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.AccommodationTypes.Queries
{
    internal static class AccommodationTypeProjections
    {
        public static readonly Expression<Func<AccommodationType, AccommodationTypeListItemDto>> ListItem =
            x => new AccommodationTypeListItemDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                IsMain = x.IsMain,
                PerRoom = x.PerRoom,
                SortOrder = x.SortOrder,
                MainPlacementRuleId = x.MainPlacementRuleId,
                ExtraPlacementRuleId = x.ExtraPlacementRuleId
            };
    }
}