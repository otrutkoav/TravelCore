using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Application.Common.Queries;
using TourCore.Application.Finance.RealCourses.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Finance.RealCourses;

namespace TourCore.Application.Finance.RealCourses.Handlers
{
    public class GetRealCoursesHandler : IQueryHandler<GetRealCoursesQuery, PagedResponseDto<RealCourseListItemDto>>
    {
        private readonly IRealCourseRepository _realCourseRepository;
        private readonly PagedQueryExecutor _pagedQueryExecutor;

        public GetRealCoursesHandler(
            IRealCourseRepository realCourseRepository,
            PagedQueryExecutor pagedQueryExecutor)
        {
            _realCourseRepository = realCourseRepository;
            _pagedQueryExecutor = pagedQueryExecutor;
        }

        public async Task<PagedResponseDto<RealCourseListItemDto>> Handle(
            GetRealCoursesQuery query,
            CancellationToken cancellationToken)
        {
            query = query ?? new GetRealCoursesQuery();

            var courses = _realCourseRepository.Query();

            if (query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.FromRateCode))
                {
                    var fromRateCode = query.Filter.FromRateCode.Trim();

                    courses = courses.Where(x => x.FromRateCode.Contains(fromRateCode));
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.ToRateCode))
                {
                    var toRateCode = query.Filter.ToRateCode.Trim();

                    courses = courses.Where(x => x.ToRateCode.Contains(toRateCode));
                }

                if (query.Filter.DateFrom.HasValue)
                {
                    var dateFrom = query.Filter.DateFrom.Value;

                    courses = courses.Where(x => x.DateBeg >= dateFrom);
                }

                if (query.Filter.DateTo.HasValue)
                {
                    var dateTo = query.Filter.DateTo.Value;

                    courses = courses.Where(x => x.DateBeg <= dateTo);
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                courses = courses
                    .OrderBy(x => x.FromRateCode)
                    .ThenBy(x => x.ToRateCode)
                    .ThenBy(x => x.DateBeg);
            }
            else
            {
                courses = courses.ApplySorting(
                    query,
                    RealCourseSortDefinition.Instance);
            }

            var dtoQuery = courses.Select(RealCourseProjections.ListItem);

            var result = await _pagedQueryExecutor.ExecuteAsync(
                dtoQuery,
                query,
                cancellationToken);

            return new PagedResponseDto<RealCourseListItemDto>(
                result.Items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}