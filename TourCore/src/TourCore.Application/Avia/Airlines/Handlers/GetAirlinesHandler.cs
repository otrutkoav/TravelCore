using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Airlines.Queries;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Avia.Airlines;
using TourCore.Contracts.Common;

namespace TourCore.Application.Avia.Airlines.Handlers
{
    public class GetAirlinesHandler : IQueryHandler<GetAirlinesQuery, PagedResponseDto<AirlineListItemDto>>
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetAirlinesHandler(
            IAirlineRepository airlineRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _airlineRepository = airlineRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<AirlineListItemDto>> Handle(
            GetAirlinesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetAirlinesQuery();

            var airlines = _airlineRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    airlines = airlines.Where(x =>
                        x.Code.Contains(search) ||
                        x.Name.Contains(search) ||
                        x.NameEn.Contains(search) ||
                        x.IcaoCode.Contains(search));
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.IcaoCode))
                {
                    var icaoCode = query.Filter.IcaoCode.Trim();

                    airlines = airlines.Where(x =>
                        x.IcaoCode.Contains(icaoCode));
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                airlines = airlines
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Code);
            }
            else
            {
                airlines = airlines.ApplySorting(
                    query,
                    AirlineSortDefinition.Instance);
            }

            var dtoQuery = airlines.Select(AirlineProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<AirlineListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}