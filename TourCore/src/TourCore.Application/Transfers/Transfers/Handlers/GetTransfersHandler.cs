using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Transfers;
using TourCore.Application.Common.Queries;
using TourCore.Application.Transfers.Transfers.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Transfers.Transfers;

namespace TourCore.Application.Transfers.Transfers.Handlers
{
    public class GetTransfersHandler
        : IQueryHandler<GetTransfersQuery, PagedResponseDto<TransferListItemDto>>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetTransfersHandler(
            ITransferRepository transferRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _transferRepository = transferRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<TransferListItemDto>> Handle(
            GetTransfersQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetTransfersQuery();

            var transfers = _transferRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    transfers = transfers.Where(x => x.Name.Contains(search));
                }

                if (query.Filter.CityId.HasValue)
                {
                    var cityId = query.Filter.CityId.Value;
                    transfers = transfers.Where(x => x.CityId == cityId);
                }

                if (query.Filter.DirectionId.HasValue)
                {
                    var directionId = query.Filter.DirectionId.Value;
                    transfers = transfers.Where(x => x.DirectionId == directionId);
                }

                if (query.Filter.IsMain.HasValue)
                {
                    var isMain = query.Filter.IsMain.Value;
                    transfers = transfers.Where(x => x.IsMain == isMain);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                transfers = transfers
                    .OrderBy(x => x.ShowOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                transfers = transfers.ApplySorting(
                    query,
                    TransferSortDefinition.Instance);
            }

            var dtoQuery = transfers.Select(TransferProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<TransferListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}