using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Transportation;
using TourCore.Application.Common.Queries;
using TourCore.Application.Transportation.Transports.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Transportation.Transports;

namespace TourCore.Application.Transportation.Transports.Handlers
{
    public class GetTransportsHandler
        : IQueryHandler<GetTransportsQuery, PagedResponseDto<TransportListItemDto>>
    {
        private readonly ITransportRepository _transportRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetTransportsHandler(
            ITransportRepository transportRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _transportRepository = transportRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<TransportListItemDto>> Handle(
            GetTransportsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetTransportsQuery();

            var transports = _transportRepository.Query();

            if (query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                transports = transports.Where(x =>
                    x.Name.Contains(search) ||
                    x.NameEn.Contains(search));
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                transports = transports
                    .OrderBy(x => x.ShowOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                transports = transports.ApplySorting(
                    query,
                    TransportSortDefinition.Instance);
            }

            var dtoQuery = transports.Select(TransportProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<TransportListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}