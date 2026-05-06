using TourCore.Application.Common.Queries;
using TourCore.Application.Railway.TrainSchedules.DTOs;

namespace TourCore.Application.Railway.TrainSchedules.Queries
{
    /// <summary>
    /// Запрос списка расписаний железнодорожных переездов.
    /// </summary>
    public class GetTrainSchedulesQuery : PagedQuery
    {
        public GetTrainSchedulesQuery()
        {
            Filter = new TrainScheduleListFilter();
        }

        /// <summary>
        /// Фильтр списка расписаний железнодорожных переездов.
        /// </summary>
        public TrainScheduleListFilter Filter { get; set; }
    }
}