using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Bus.BusTransferPoints.Queries;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Bus.BusTransferPoints;
using TourCore.Contracts.Common;

namespace TourCore.Application.Bus.BusTransferPoints.Handlers
{
    public class GetBusTransferPointsHandler
        : IQueryHandler<GetBusTransferPointsQuery, PagedResponseDto<BusTransferPointListItemDto>>
    {
        private readonly IBusTransferPointRepository _busTransferPointRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetBusTransferPointsHandler(
            IBusTransferPointRepository busTransferPointRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _busTransferPointRepository = busTransferPointRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<BusTransferPointListItemDto>> Handle(
            GetBusTransferPointsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetBusTransferPointsQuery();

            var points = _busTransferPointRepository.Query();

            if (query.Filter != null)
            {
                if (query.Filter.BusTransferId.HasValue)
                {
                    var busTransferId = query.Filter.BusTransferId.Value;
                    points = points.Where(x => x.BusTransferId == busTransferId);
                }

                if (query.Filter.CountryFromId.HasValue)
                {
                    var countryFromId = query.Filter.CountryFromId.Value;
                    points = points.Where(x => x.CountryFromId == countryFromId);
                }

                if (query.Filter.CityFromId.HasValue)
                {
                    var cityFromId = query.Filter.CityFromId.Value;
                    points = points.Where(x => x.CityFromId == cityFromId);
                }

                if (query.Filter.CountryToId.HasValue)
                {
                    var countryToId = query.Filter.CountryToId.Value;
                    points = points.Where(x => x.CountryToId == countryToId);
                }

                if (query.Filter.CityToId.HasValue)
                {
                    var cityToId = query.Filter.CityToId.Value;
                    points = points.Where(x => x.CityToId == cityToId);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                points = points
                    .OrderBy(x => x.BusTransferId)
                    .ThenBy(x => x.CountryFromId)
                    .ThenBy(x => x.CityFromId)
                    .ThenBy(x => x.CountryToId)
                    .ThenBy(x => x.CityToId);
            }
            else
            {
                points = points.ApplySorting(
                    query,
                    BusTransferPointSortDefinition.Instance);
            }

            var dtoQuery = points.Select(BusTransferPointProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<BusTransferPointListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}