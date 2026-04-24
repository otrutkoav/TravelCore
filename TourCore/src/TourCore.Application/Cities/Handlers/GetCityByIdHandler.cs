using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Geography.Cities;
using TourCore.Application.Cities.Mappings;
using TourCore.Application.Cities.Queries;
using TourCore.Application.Common.Exceptions;

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
                throw new NotFoundException("City was not found.");

            return entity.ToDto();
        }
    }
}