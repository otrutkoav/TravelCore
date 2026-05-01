using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Cities.Mappings;
using TourCore.Application.Cities.Queries;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Geography.Cities;

namespace TourCore.Application.Cities.Handlers
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