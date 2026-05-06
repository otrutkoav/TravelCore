using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Seating;
using TourCore.Application.Common.Models;
using TourCore.Application.Seating.SeatingCells.Mappings;
using TourCore.Application.Seating.SeatingCells.Queries;
using TourCore.Contracts.Seating.SeatingCells;

namespace TourCore.Application.Seating.SeatingCells.Handlers
{
    public class GetSeatingCellsHandler : IQueryHandler<GetSeatingCellsQuery, ListResult<SeatingCellListItemDto>>
    {
        private readonly ISeatingCellRepository _seatingCellRepository;

        public GetSeatingCellsHandler(ISeatingCellRepository seatingCellRepository)
        {
            _seatingCellRepository = seatingCellRepository;
        }

        public async Task<ListResult<SeatingCellListItemDto>> Handle(GetSeatingCellsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _seatingCellRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null && query.Filter.VehiclePlanId.HasValue)
                items = items.Where(x => x.VehiclePlanId == query.Filter.VehiclePlanId.Value);

            var result = items
                .OrderBy(x => x.VehiclePlanId)
                .ThenBy(x => x.Index)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<SeatingCellListItemDto>.Create(result);
        }
    }
}