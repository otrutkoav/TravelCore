using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Geography.Countries.Mappings;
using TourCore.Application.Geography.Countries.Queries;
using TourCore.Contracts.Geography.Countries;

namespace TourCore.Application.Geography.Countries.Handlers
{
    public class GetCountryByIdHandler : IQueryHandler<GetCountryByIdQuery, CountryDto>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountryByIdHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<CountryDto> Handle(GetCountryByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _countryRepository.GetByIdAsync(query.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(ErrorMessages.CountryNotFound, ErrorCode.CountryNotFound);

            return entity.ToDto();
        }
    }
}