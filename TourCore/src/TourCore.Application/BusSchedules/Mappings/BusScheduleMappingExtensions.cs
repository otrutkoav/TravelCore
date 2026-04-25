using TourCore.Contracts.Bus.BusSchedules;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.BusSchedules.Mappings
{
    public static class BusScheduleMappingExtensions
    {
        public static BusScheduleDto ToDto(this BusSchedule entity)
        {
            return new BusScheduleDto
            {
                Id = entity.Id,
                BusTransferId = entity.BusTransferId,
                DateFrom = entity.DateFrom,
                DateTo = entity.DateTo,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                DaysOfWeek = entity.DaysOfWeek == null ? null : entity.DaysOfWeek.ToLegacy(),
                DaysOnRoad = entity.DaysOnRoad
            };
        }

        public static BusScheduleListItemDto ToListItemDto(this BusSchedule entity)
        {
            return new BusScheduleListItemDto
            {
                Id = entity.Id,
                BusTransferId = entity.BusTransferId,
                DateFrom = entity.DateFrom,
                DateTo = entity.DateTo,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                DaysOfWeek = entity.DaysOfWeek == null ? null : entity.DaysOfWeek.ToLegacy(),
                DaysOnRoad = entity.DaysOnRoad
            };
        }
    }
}