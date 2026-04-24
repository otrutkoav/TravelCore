using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RealCourses.DTOs;
using TourCore.Application.RealCourses.Mappings;
using TourCore.Application.RealCourses.Queries;

namespace TourCore.Application.RealCourses.Handlers
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