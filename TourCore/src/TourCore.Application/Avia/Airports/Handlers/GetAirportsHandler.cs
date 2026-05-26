using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Airports.Queries;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Avia.Airports;
using TourCore.Contracts.Common;

namespace TourCore.Application.Avia.Airports.Handlers
{
    public class GetAirportsHandler : IQueryHandler<GetAirportsQuery, PagedResponseDto<AirportListItemDto>>
    {
        private readonly IAirportRepository _airportRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetAirportsHandler(
            IAirportRepository airportRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _airportRepository = airportRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<AirportListItemDto>> Handle(
            GetAirportsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetAirportsQuery();

            var airports = _airportRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    airports = airports.Where(x =>
                        x.Code.Contains(search) ||
                        x.Name.Contains(search) ||
                        x.NameEn.Contains(search) ||
                        x.IcaoCode.Contains(search) ||
                        x.LetterCode.Contains(search));
                }

                if (query.Filter.CityId.HasValue)
                {
                    airports = airports.Where(x =>
                        x.CityId == query.Filter.CityId.Value);
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.IcaoCode))
                {
                    var icaoCode = query.Filter.IcaoCode.Trim();

                    airports = airports.Where(x =>
                        x.IcaoCode.Contains(icaoCode));
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                airports = airports
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Code);
            }
            else
            {
                airports = airports.ApplySorting(
                    query,
                    AirportSortDefinition.Instance);
            }

            var dtoQuery = airports.Select(AirportProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<AirportListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}