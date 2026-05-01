using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Geography.Cities.Mappings;
using TourCore.Application.Geography.Cities.Queries;
using TourCore.Contracts.Geography.Cities;

namespace TourCore.Application.Geography.Cities.Handlers
{
    public class GetCityByIdHandler : IQueryHandler<GetCityByIdQuery, CityDto>
    {
        private readonly ICityRepository _cityRepository;

        public GetCityByIdHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<CityDto> Handle(GetCityByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _cityRepository.GetByIdAsync(query.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(ErrorMessages.CityNotFound, ErrorCode.CityNotFound);

            return entity.ToDto();
        }
    }
}