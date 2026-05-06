using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Railway;
using TourCore.Application.Common.Queries;
using TourCore.Application.Railway.RailwayTransfers.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Railway.RailwayTransfers;

namespace TourCore.Application.Railway.RailwayTransfers.Handlers
{
    public class GetRailwayTransfersHandler
        : IQueryHandler<GetRailwayTransfersQuery, PagedResponseDto<RailwayTransferListItemDto>>
    {
        private readonly IRailwayTransferRepository _railwayTransferRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetRailwayTransfersHandler(
            IRailwayTransferRepository railwayTransferRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _railwayTransferRepository = railwayTransferRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<RailwayTransferListItemDto>> Handle(
            GetRailwayTransfersQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetRailwayTransfersQuery();

            var railwayTransfers = _railwayTransferRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    railwayTransfers = railwayTransfers.Where(x => x.Name.Contains(search));
                }

                if (query.Filter.CountryFromId.HasValue)
                {
                    var countryFromId = query.Filter.CountryFromId.Value;
                    railwayTransfers = railwayTransfers.Where(x => x.CountryFromId == countryFromId);
                }

                if (query.Filter.CityFromId.HasValue)
                {
                    var cityFromId = query.Filter.CityFromId.Value;
                    railwayTransfers = railwayTransfers.Where(x => x.CityFromId == cityFromId);
                }

                if (query.Filter.CountryToId.HasValue)
                {
                    var countryToId = query.Filter.CountryToId.Value;
                    railwayTransfers = railwayTransfers.Where(x => x.CountryToId == countryToId);
                }

                if (query.Filter.CityToId.HasValue)
                {
                    var cityToId = query.Filter.CityToId.Value;
                    railwayTransfers = railwayTransfers.Where(x => x.CityToId == cityToId);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                railwayTransfers = railwayTransfers
                    .OrderBy(x => x.CountryFromId)
                    .ThenBy(x => x.CityFromId)
                    .ThenBy(x => x.CountryToId)
                    .ThenBy(x => x.CityToId)
                    .ThenBy(x => x.Name);
            }
            else
            {
                railwayTransfers = railwayTransfers.ApplySorting(
                    query,
                    RailwayTransferSortDefinition.Instance);
            }

            var dtoQuery = railwayTransfers.Select(RailwayTransferProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<RailwayTransferListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}