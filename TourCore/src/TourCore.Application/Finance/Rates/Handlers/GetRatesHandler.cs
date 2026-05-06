using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Application.Common.Queries;
using TourCore.Application.Finance.Rates.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Finance.Rates;

namespace TourCore.Application.Finance.Rates.Handlers
{
    public class GetRatesHandler : IQueryHandler<GetRatesQuery, PagedResponseDto<RateListItemDto>>
    {
        private readonly IRateRepository _rateRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetRatesHandler(
            IRateRepository rateRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _rateRepository = rateRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<RateListItemDto>> Handle(
            GetRatesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetRatesQuery();

            var rates = _rateRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    rates = rates.Where(x =>
                        x.Code.Contains(search) ||
                        x.Name.Contains(search) ||
                        x.IsoCode.Contains(search));
                }

                if (query.Filter.IsMain.HasValue)
                {
                    var isMain = query.Filter.IsMain.Value;
                    rates = rates.Where(x => x.IsMain == isMain);
                }

                if (query.Filter.IsNational.HasValue)
                {
                    var isNational = query.Filter.IsNational.Value;
                    rates = rates.Where(x => x.IsNational == isNational);
                }

                if (query.Filter.ShowInSearch.HasValue)
                {
                    var showInSearch = query.Filter.ShowInSearch.Value;
                    rates = rates.Where(x => x.ShowInSearch == showInSearch);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                rates = rates
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Code);
            }
            else
            {
                rates = rates.ApplySorting(
                    query,
                    RateSortDefinition.Instance);
            }

            var dtoQuery = rates.Select(RateProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<RateListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}