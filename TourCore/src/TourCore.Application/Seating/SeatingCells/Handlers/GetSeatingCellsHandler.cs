using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Seating;
using TourCore.Application.Common.Queries;
using TourCore.Application.Seating.SeatingCells.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Seating.SeatingCells;

namespace TourCore.Application.Seating.SeatingCells.Handlers
{
    public class GetSeatingCellsHandler : IQueryHandler<GetSeatingCellsQuery, PagedResponseDto<SeatingCellListItemDto>>
    {
        private readonly ISeatingCellRepository _seatingCellRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetSeatingCellsHandler(
            ISeatingCellRepository seatingCellRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _seatingCellRepository = seatingCellRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<SeatingCellListItemDto>> Handle(
            GetSeatingCellsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetSeatingCellsQuery();

            var seatingCells = _seatingCellRepository.Query();

            if (query.Filter != null)
            {
                if (query.Filter.VehiclePlanId.HasValue)
                {
                    seatingCells = seatingCells.Where(x =>
                        x.VehiclePlanId == query.Filter.VehiclePlanId.Value);
                }

                if (query.Filter.Type.HasValue)
                {
                    var type = (TourCore.Domain.Seating.Entities.SeatType)(int)query.Filter.Type.Value;

                    seatingCells = seatingCells.Where(x =>
                        x.Type == type);
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.Number))
                {
                    var number = query.Filter.Number.Trim();

                    seatingCells = seatingCells.Where(x =>
                        x.Number.Contains(number));
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                seatingCells = seatingCells
                    .OrderBy(x => x.VehiclePlanId)
                    .ThenBy(x => x.Index);
            }
            else
            {
                seatingCells = seatingCells.ApplySorting(
                    query,
                    SeatingCellSortDefinition.Instance);
            }

            var dtoQuery = seatingCells.Select(SeatingCellProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<SeatingCellListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}