using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Finance.RealCourses;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Application.Finance.RealCourses.Mappings;
using TourCore.Application.Finance.RealCourses.Queries;

namespace TourCore.Application.Finance.RealCourses.Handlers
{
    public class GetRealCourseByIdHandler : IQueryHandler<GetRealCourseByIdQuery, RealCourseDto>
    {
        private readonly IRealCourseRepository _realCourseRepository;

        public GetRealCourseByIdHandler(IRealCourseRepository realCourseRepository)
        {
            _realCourseRepository = realCourseRepository;
        }

        public async Task<RealCourseDto> Handle(GetRealCourseByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _realCourseRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Real course was not found.");

            return entity.ToDto();
        }
    }
}