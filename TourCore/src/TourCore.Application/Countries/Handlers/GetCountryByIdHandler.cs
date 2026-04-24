using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Geography.Countries;
using TourCore.Application.Countries.Mappings;
using TourCore.Application.Countries.Queries;

namespace TourCore.Application.Countries.Handlers
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
                throw new NotFoundException("Country was not found.");

            return entity.ToDto();
        }
    }
}