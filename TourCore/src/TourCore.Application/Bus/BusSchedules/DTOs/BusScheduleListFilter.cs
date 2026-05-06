namespace TourCore.Application.Bus.BusSchedules.DTOs
{
    /// <summary>
    /// Фильтр списка расписаний автобусных переездов.
    /// </summary>
    public class BusScheduleListFilter
    {
        /// <summary>
        /// Идентификатор автобусного переезда.
        /// </summary>
        public int? BusTransferId { get; set; }
    }
}