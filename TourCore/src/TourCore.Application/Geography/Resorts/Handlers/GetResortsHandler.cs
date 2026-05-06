using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Common.Queries;
using TourCore.Application.Geography.Resorts.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Geography.Resorts;

namespace TourCore.Application.Geography.Resorts.Handlers
{
    public class GetResortsHandler : IQueryHandler<GetResortsQuery, PagedResponseDto<ResortListItemDto>>
    {
        private readonly IResortRepository _resortRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetResortsHandler(
            IResortRepository resortRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _resortRepository = resortRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<ResortListItemDto>> Handle(
            GetResortsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetResortsQuery();

            var resorts = _resortRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    resorts = resorts.Where(x =>
                        x.Name.Contains(search) ||
                        x.NameEn.Contains(search));
                }

                if (query.Filter.CountryId.HasValue)
                {
                    var countryId = query.Filter.CountryId.Value;

                    resorts = resorts.Where(x => x.CountryId == countryId);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                resorts = resorts.OrderBy(x => x.Name);
            }
            else
            {
                resorts = resorts.ApplySorting(
                    query,
                    ResortSortDefinition.Instance);
            }

            var dtoQuery = resorts.Select(ResortProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<ResortListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}