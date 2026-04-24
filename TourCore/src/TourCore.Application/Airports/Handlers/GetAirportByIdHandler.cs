using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Avia.Airports;
using TourCore.Application.Airports.Mappings;
using TourCore.Application.Airports.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence;

namespace TourCore.Application.Airports.Handlers
{
    public class GetAirportByIdHandler : IQueryHandler<GetAirportByIdQuery, AirportDto>
    {
        private readonly IAirportRepository _airportRepository;

        public GetAirportByIdHandler(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<AirportDto> Handle(GetAirportByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _airportRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Airport was not found.");

            return entity.ToDto();
        }
    }
}