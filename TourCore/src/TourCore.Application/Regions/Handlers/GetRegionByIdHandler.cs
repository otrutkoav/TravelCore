using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Regions.Mappings;
using TourCore.Application.Regions.Queries;
using TourCore.Contracts.Geography.Regions;

namespace TourCore.Application.Regions.Handlers
{
    public class GetRegionByIdHandler : IQueryHandler<GetRegionByIdQuery, RegionDto>
    {
        private readonly IRegionRepository _regionRepository;

        public GetRegionByIdHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<RegionDto> Handle(GetRegionByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _regionRepository.GetByIdAsync(query.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(ErrorMessages.RegionNotFound, ErrorCode.RegionNotFound);

            return entity.ToDto();
        }
    }
}