using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Application.SeatingCells.DTOs;
using TourCore.Application.SeatingCells.Mappings;
using TourCore.Application.SeatingCells.Queries;

namespace TourCore.Application.SeatingCells.Handlers
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

            if (query != null && query.Filter != null)
            {
                if (query.Filter.VehiclePlanId.HasValue)
                    items = items.Where(x => x.VehiclePlanId == query.Filter.VehiclePlanId.Value);

                if (query.Filter.Type.HasValue)
                    items = items.Where(x => x.Type == query.Filter.Type.Value);

                if (!string.IsNullOrWhiteSpace(query.Filter.Number))
                {
                    var number = query.Filter.Number.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.Number) &&
                        x.Number.IndexOf(number, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            var result = items
                .OrderBy(x => x.VehiclePlanId)
                .ThenBy(x => x.Index)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<SeatingCellListItemDto>.Create(result);
        }
    }
}