using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Application.Aircrafts.Mappings;
using TourCore.Application.Aircrafts.Queries;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Aircrafts.Handlers
{
    public class GetAircraftByIdHandler : IQueryHandler<GetAircraftByIdQuery, AircraftDto>
    {
        private readonly IAircraftRepository _aircraftRepository;

        public GetAircraftByIdHandler(IAircraftRepository aircraftRepository)
        {
            _aircraftRepository = aircraftRepository;
        }

        public async Task<AircraftDto> Handle(GetAircraftByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _aircraftRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Aircraft was not found.");

            return entity.ToDto();
        }
    }
}