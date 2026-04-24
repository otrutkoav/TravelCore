using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Application.Aircrafts.Mappings;
using TourCore.Application.Aircrafts.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.Aircrafts.Handlers
{
    public class GetAircraftsHandler : IQueryHandler<GetAircraftsQuery, ListResult<AircraftListItemDto>>
    {
        private readonly IAircraftRepository _aircraftRepository;

        public GetAircraftsHandler(IAircraftRepository aircraftRepository)
        {
            _aircraftRepository = aircraftRepository;
        }

        public async Task<ListResult<AircraftListItemDto>> Handle(GetAircraftsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _aircraftRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                items = items.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Code) && x.Code.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            var result = items
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Code)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<AircraftListItemDto>.Create(result);
        }
    }
}