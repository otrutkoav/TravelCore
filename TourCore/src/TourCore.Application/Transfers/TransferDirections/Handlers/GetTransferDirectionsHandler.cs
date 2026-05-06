using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Transfers;
using TourCore.Application.Common.Queries;
using TourCore.Application.Transfers.TransferDirections.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Transfers.TransferDirections;

namespace TourCore.Application.Transfers.TransferDirections.Handlers
{
    public class GetTransferDirectionsHandler
        : IQueryHandler<GetTransferDirectionsQuery, PagedResponseDto<TransferDirectionListItemDto>>
    {
        private readonly ITransferDirectionRepository _transferDirectionRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetTransferDirectionsHandler(
            ITransferDirectionRepository transferDirectionRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _transferDirectionRepository = transferDirectionRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<TransferDirectionListItemDto>> Handle(
            GetTransferDirectionsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetTransferDirectionsQuery();

            var directions = _transferDirectionRepository.Query();

            if (query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                directions = directions.Where(x => x.Name.Contains(search));
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                directions = directions.OrderBy(x => x.Name);
            }
            else
            {
                directions = directions.ApplySorting(
                    query,
                    TransferDirectionSortDefinition.Instance);
            }

            var dtoQuery = directions.Select(TransferDirectionProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<TransferDirectionListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}