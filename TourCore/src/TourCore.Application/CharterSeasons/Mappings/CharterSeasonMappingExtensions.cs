using TourCore.Contracts.Avia.CharterSeasons;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.CharterSeasons.Mappings
{
    public static class CharterSeasonMappingExtensions
    {
        public static CharterSeasonDto ToDto(this CharterSeason entity)
        {
            return new CharterSeasonDto
            {
                Id = entity.Id,
                CharterId = entity.CharterId,
                DateFrom = entity.DateFrom,
                DateTo = entity.DateTo,
                DaysOfWeek = entity.DaysOfWeek == null ? null : entity.DaysOfWeek.ToLegacy(),
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                IsNextDayArrival = entity.IsNextDayArrival,
                Remark = entity.Remark
            };
        }

        public static CharterSeasonListItemDto ToListItemDto(this CharterSeason entity)
        {
            return new CharterSeasonListItemDto
            {
                Id = entity.Id,
                CharterId = entity.CharterId,
                DateFrom = entity.DateFrom,
                DateTo = entity.DateTo,
                DaysOfWeek = entity.DaysOfWeek == null ? null : entity.DaysOfWeek.ToLegacy(),
                IsNextDayArrival = entity.IsNextDayArrival,
                Remark = entity.Remark
            };
        }
    }
}