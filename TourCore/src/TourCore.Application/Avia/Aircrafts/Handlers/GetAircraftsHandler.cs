using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Aircrafts.Queries;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Contracts.Common;

namespace TourCore.Application.Avia.Aircrafts.Handlers
{
    public class GetAircraftsHandler : IQueryHandler<GetAircraftsQuery, PagedResponseDto<AircraftListItemDto>>
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetAircraftsHandler(
            IAircraftRepository aircraftRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _aircraftRepository = aircraftRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<AircraftListItemDto>> Handle(
            GetAircraftsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetAircraftsQuery();

            var aircrafts = _aircraftRepository.Query();

            if (query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                aircrafts = aircrafts.Where(x =>
                    x.Code.Contains(search) ||
                    x.Name.Contains(search) ||
                    x.NameEn.Contains(search));
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                aircrafts = aircrafts
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Code);
            }
            else
            {
                aircrafts = aircrafts.ApplySorting(
                    query,
                    AircraftSortDefinition.Instance);
            }

            var dtoQuery = aircrafts.Select(AircraftProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<AircraftListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}