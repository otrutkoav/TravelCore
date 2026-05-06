using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Common.Queries;
using TourCore.Application.Geography.Regions.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Geography.Regions;

namespace TourCore.Application.Geography.Regions.Handlers
{
    public class GetRegionsHandler : IQueryHandler<GetRegionsQuery, PagedResponseDto<RegionListItemDto>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetRegionsHandler(
            IRegionRepository regionRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _regionRepository = regionRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<RegionListItemDto>> Handle(
            GetRegionsQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetRegionsQuery();

            var regions = _regionRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    regions = regions.Where(x =>
                        x.Name.Contains(search) ||
                        x.NameEn.Contains(search) ||
                        x.Code.Contains(search));
                }

                if (query.Filter.CountryId.HasValue)
                {
                    var countryId = query.Filter.CountryId.Value;

                    regions = regions.Where(x => x.CountryId == countryId);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                regions = regions
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                regions = regions.ApplySorting(
                    query,
                    RegionSortDefinition.Instance);
            }

            var dtoQuery = regions.Select(RegionProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<RegionListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}