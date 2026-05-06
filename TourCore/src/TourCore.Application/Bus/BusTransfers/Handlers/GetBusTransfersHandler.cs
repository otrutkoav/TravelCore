using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Bus.BusTransfers.Queries;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Bus.BusTransfers;
using TourCore.Contracts.Common;

namespace TourCore.Application.Bus.BusTransfers.Handlers
{
    public class GetBusTransfersHandler : IQueryHandler<GetBusTransfersQuery, PagedResponseDto<BusTransferListItemDto>>
    {
        private readonly IBusTransferRepository _busTransferRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetBusTransfersHandler(
            IBusTransferRepository busTransferRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _busTransferRepository = busTransferRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<BusTransferListItemDto>> Handle(
            GetBusTransfersQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetBusTransfersQuery();

            var busTransfers = _busTransferRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    busTransfers = busTransfers.Where(x => x.Name.Contains(search));
                }

                if (query.Filter.CountryFromId.HasValue)
                {
                    var countryFromId = query.Filter.CountryFromId.Value;
                    busTransfers = busTransfers.Where(x => x.CountryFromId == countryFromId);
                }

                if (query.Filter.CityFromId.HasValue)
                {
                    var cityFromId = query.Filter.CityFromId.Value;
                    busTransfers = busTransfers.Where(x => x.CityFromId == cityFromId);
                }

                if (query.Filter.CountryToId.HasValue)
                {
                    var countryToId = query.Filter.CountryToId.Value;
                    busTransfers = busTransfers.Where(x => x.CountryToId == countryToId);
                }

                if (query.Filter.CityToId.HasValue)
                {
                    var cityToId = query.Filter.CityToId.Value;
                    busTransfers = busTransfers.Where(x => x.CityToId == cityToId);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                busTransfers = busTransfers
                    .OrderBy(x => x.CountryFromId)
                    .ThenBy(x => x.CityFromId)
                    .ThenBy(x => x.CountryToId)
                    .ThenBy(x => x.CityToId)
                    .ThenBy(x => x.Name);
            }
            else
            {
                busTransfers = busTransfers.ApplySorting(
                    query,
                    BusTransferSortDefinition.Instance);
            }

            var dtoQuery = busTransfers.Select(BusTransferProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<BusTransferListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}