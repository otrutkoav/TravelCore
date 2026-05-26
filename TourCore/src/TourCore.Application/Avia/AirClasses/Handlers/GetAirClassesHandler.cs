using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.AirClasses.Queries;
using TourCore.Application.Common.Queries;
using TourCore.Contracts.Avia.AirClasses;
using TourCore.Contracts.Common;

namespace TourCore.Application.Avia.AirClasses.Handlers
{
    public class GetAirClassesHandler : IQueryHandler<GetAirClassesQuery, PagedResponseDto<AirClassListItemDto>>
    {
        private readonly IAirClassRepository _airClassRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetAirClassesHandler(
            IAirClassRepository airClassRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _airClassRepository = airClassRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<AirClassListItemDto>> Handle(
            GetAirClassesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetAirClassesQuery();

            var airClasses = _airClassRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    airClasses = airClasses.Where(x =>
                        x.Code.Contains(search) ||
                        x.Name.Contains(search) ||
                        x.NameEn.Contains(search));
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.Group))
                {
                    var group = query.Filter.Group.Trim();

                    airClasses = airClasses.Where(x =>
                        x.Group.Contains(group));
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                airClasses = airClasses
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name);
            }
            else
            {
                airClasses = airClasses.ApplySorting(
                    query,
                    AirClassSortDefinition.Instance);
            }

            var dtoQuery = airClasses.Select(AirClassProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<AirClassListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}