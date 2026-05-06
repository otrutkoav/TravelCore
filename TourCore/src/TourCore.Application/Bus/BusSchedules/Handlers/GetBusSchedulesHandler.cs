using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Bus.BusSchedules.Queries;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Bus.BusSchedules;
using TourCore.Contracts.Common;

namespace TourCore.Application.Bus.BusSchedules.Handlers
{
    public class GetBusSchedulesHandler
        : IQueryHandler<GetBusSchedulesQuery, PagedResponseDto<BusScheduleListItemDto>>
    {
        private readonly IBusScheduleRepository _busScheduleRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetBusSchedulesHandler(
            IBusScheduleRepository busScheduleRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _busScheduleRepository = busScheduleRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<BusScheduleListItemDto>> Handle(
            GetBusSchedulesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetBusSchedulesQuery();

            var schedules = _busScheduleRepository.Query();

            if (query.Filter != null && query.Filter.BusTransferId.HasValue)
            {
                var busTransferId = query.Filter.BusTransferId.Value;

                schedules = schedules.Where(x => x.BusTransferId == busTransferId);
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                schedules = schedules
                    .OrderBy(x => x.BusTransferId)
                    .ThenBy(x => x.DateFrom)
                    .ThenBy(x => x.DateTo);
            }
            else
            {
                schedules = schedules.ApplySorting(
                    query,
                    BusScheduleSortDefinition.Instance);
            }

            var pagedEntities = await _pagedQueryExecutor.ExecuteAsync(
                schedules,
                query,
                cancellationToken);

            var items = pagedEntities.Items
                .Select(x => new BusScheduleListItemDto
                {
                    Id = x.Id,
                    BusTransferId = x.BusTransferId,
                    DateFrom = x.DateFrom,
                    DateTo = x.DateTo,
                    TimeFrom = x.TimeFrom,
                    TimeTo = x.TimeTo,
                    DaysOfWeek = x.DaysOfWeek == null ? null : x.DaysOfWeek.ToLegacy(),
                    DaysOnRoad = x.DaysOnRoad
                })
                .ToArray();

            return new PagedResponseDto<BusScheduleListItemDto>(
                items,
                pagedEntities.Page,
                pagedEntities.PageSize,
                pagedEntities.TotalCount);
        }
    }
}