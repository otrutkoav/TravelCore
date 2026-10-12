using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Avia.Airports;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Airports.Mappings;
using TourCore.Application.Avia.Airports.Queries;
using TourCore.Application.Common.Errors;

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
                throw new NotFoundException(ErrorMessages.AirportNotFound, ErrorCode.AirportNotFound);

            return entity.ToDto();
        }
    }
}