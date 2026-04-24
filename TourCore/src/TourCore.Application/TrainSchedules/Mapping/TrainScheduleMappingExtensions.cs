using TourCore.Contracts.Railway.TrainSchedules;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.TrainSchedules.Mappings
{
    public static class TrainScheduleMappingExtensions
    {
        public static TrainScheduleDto ToDto(this TrainSchedule entity)
        {
            return new TrainScheduleDto
            {
                Id = entity.Id,
                RailwayTransferId = entity.RailwayTransferId,
                DateFrom = entity.DateFrom,
                DateTo = entity.DateTo,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                DaysOfWeek = entity.DaysOfWeek == null ? null : entity.DaysOfWeek.ToLegacy(),
                DaysOnRoad = entity.DaysOnRoad,
                Remark = entity.Remark
            };
        }

        public static TrainScheduleListItemDto ToListItemDto(this TrainSchedule entity)
        {
            return new TrainScheduleListItemDto
            {
                Id = entity.Id,
                RailwayTransferId = entity.RailwayTransferId,
                Remark = entity.Remark
            };
        }
    }
}