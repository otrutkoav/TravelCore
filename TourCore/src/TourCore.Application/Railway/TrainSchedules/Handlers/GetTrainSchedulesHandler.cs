using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Railway;
using TourCore.Application.Common.Queries;
using TourCore.Application.Railway.TrainSchedules.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Railway.TrainSchedules;

namespace TourCore.Application.Railway.TrainSchedules.Handlers
{
    public class GetTrainSchedulesHandler
        : IQueryHandler<GetTrainSchedulesQuery, PagedResponseDto<TrainScheduleListItemDto>>
    {
        private readonly ITrainScheduleRepository _trainScheduleRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetTrainSchedulesHandler(
            ITrainScheduleRepository trainScheduleRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _trainScheduleRepository = trainScheduleRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<TrainScheduleListItemDto>> Handle(
            GetTrainSchedulesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetTrainSchedulesQuery();

            var schedules = _trainScheduleRepository.Query();

            if (query.Filter != null && query.Filter.RailwayTransferId.HasValue)
            {
                var railwayTransferId = query.Filter.RailwayTransferId.Value;

                schedules = schedules.Where(x => x.RailwayTransferId == railwayTransferId);
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                schedules = schedules
                    .OrderBy(x => x.RailwayTransferId)
                    .ThenBy(x => x.DateFrom)
                    .ThenBy(x => x.DateTo);
            }
            else
            {
                schedules = schedules.ApplySorting(
                    query,
                    TrainScheduleSortDefinition.Instance);
            }

            var pagedEntities = await _pagedQueryExecutor.ExecuteAsync(
                schedules,
                query,
                cancellationToken);

            var items = pagedEntities.Items
                .Select(x => new TrainScheduleListItemDto
                {
                    Id = x.Id,
                    RailwayTransferId = x.RailwayTransferId,
                    DateFrom = x.DateFrom,
                    DateTo = x.DateTo,
                    TimeFrom = x.TimeFrom,
                    TimeTo = x.TimeTo,
                    DaysOfWeek = x.DaysOfWeek == null ? null : x.DaysOfWeek.ToLegacy(),
                    DaysOnRoad = x.DaysOnRoad,
                    Remark = x.Remark
                })
                .ToArray();

            return new PagedResponseDto<TrainScheduleListItemDto>(
                items,
                pagedEntities.Page,
                pagedEntities.PageSize,
                pagedEntities.TotalCount);
        }
    }
}