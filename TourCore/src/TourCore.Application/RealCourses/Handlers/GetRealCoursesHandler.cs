using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Finance.RealCourses;
using TourCore.Application.RealCourses.Mappings;
using TourCore.Application.RealCourses.Queries;

namespace TourCore.Application.RealCourses.Handlers
{
    public class GetRealCoursesHandler : IQueryHandler<GetRealCoursesQuery, ListResult<RealCourseListItemDto>>
    {
        private readonly IRealCourseRepository _realCourseRepository;

        public GetRealCoursesHandler(IRealCourseRepository realCourseRepository)
        {
            _realCourseRepository = realCourseRepository;
        }

        public async Task<ListResult<RealCourseListItemDto>> Handle(GetRealCoursesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _realCourseRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.FromRateCode))
                {
                    var fromRateCode = query.Filter.FromRateCode.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.FromRateCode) &&
                        x.FromRateCode.IndexOf(fromRateCode, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                if (!string.IsNullOrWhiteSpace(query.Filter.ToRateCode))
                {
                    var toRateCode = query.Filter.ToRateCode.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.ToRateCode) &&
                        x.ToRateCode.IndexOf(toRateCode, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            var result = items
                .OrderBy(x => x.FromRateCode)
                .ThenBy(x => x.ToRateCode)
                .ThenBy(x => x.DateBeg)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<RealCourseListItemDto>.Create(result);
        }
    }
}