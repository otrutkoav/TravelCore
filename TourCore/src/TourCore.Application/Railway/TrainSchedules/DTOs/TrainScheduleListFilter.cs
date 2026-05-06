namespace TourCore.Application.Railway.TrainSchedules.DTOs
{
    /// <summary>
    /// Фильтр списка расписаний железнодорожных переездов.
    /// </summary>
    public class TrainScheduleListFilter
    {
        /// <summary>
        /// Идентификатор железнодорожного переезда.
        /// </summary>
        public int? RailwayTransferId { get; set; }
    }
}