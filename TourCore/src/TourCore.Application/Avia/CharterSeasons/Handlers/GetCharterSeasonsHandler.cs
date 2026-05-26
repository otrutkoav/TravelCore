using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.CharterSeasons.Queries;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Avia.CharterSeasons;
using TourCore.Contracts.Common;

namespace TourCore.Application.Avia.CharterSeasons.Handlers
{
    public class GetCharterSeasonsHandler : IQueryHandler<GetCharterSeasonsQuery, PagedResponseDto<CharterSeasonListItemDto>>
    {
        private readonly ICharterSeasonRepository _charterSeasonRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetCharterSeasonsHandler(
            ICharterSeasonRepository charterSeasonRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _charterSeasonRepository = charterSeasonRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<CharterSeasonListItemDto>> Handle(
            GetCharterSeasonsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetCharterSeasonsQuery();

            var charterSeasons = _charterSeasonRepository.Query();

            if (query.Filter != null)
            {
                if (query.Filter.CharterId.HasValue)
                {
                    charterSeasons = charterSeasons.Where(x =>
                        x.CharterId == query.Filter.CharterId.Value);
                }

                if (query.Filter.DateFrom.HasValue)
                {
                    var dateFrom = query.Filter.DateFrom.Value.Date;

                    charterSeasons = charterSeasons.Where(x =>
                        x.DateFrom.HasValue &&
                        x.DateFrom.Value >= dateFrom);
                }

                if (query.Filter.DateTo.HasValue)
                {
                    var dateTo = query.Filter.DateTo.Value.Date;

                    charterSeasons = charterSeasons.Where(x =>
                        x.DateTo.HasValue &&
                        x.DateTo.Value <= dateTo);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                charterSeasons = charterSeasons
                    .OrderBy(x => x.CharterId)
                    .ThenBy(x => x.DateFrom)
                    .ThenBy(x => x.DateTo);
            }
            else
            {
                charterSeasons = charterSeasons.ApplySorting(
                    query,
                    CharterSeasonSortDefinition.Instance);
            }

            var dtoQuery = charterSeasons.Select(CharterSeasonProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<CharterSeasonListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}