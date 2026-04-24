using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Geography.Regions;
using TourCore.Application.Regions.Mappings;
using TourCore.Application.Regions.Queries;

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
                throw new NotFoundException("Region was not found.");

            return entity.ToDto();
        }
    }
}