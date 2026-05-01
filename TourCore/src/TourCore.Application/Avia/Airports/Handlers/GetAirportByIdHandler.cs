using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Avia.Airports;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Airports.Mappings;
using TourCore.Application.Avia.Airports.Queries;

namespace TourCore.Application.Avia.Airports.Handlers
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