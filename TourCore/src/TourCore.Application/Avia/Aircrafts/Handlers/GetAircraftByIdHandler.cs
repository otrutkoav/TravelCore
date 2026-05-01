using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Aircrafts.Mappings;
using TourCore.Application.Avia.Aircrafts.Queries;

namespace TourCore.Application.Avia.Aircrafts.Handlers
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