using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Avia.Airlines;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Airlines.Mappings;
using TourCore.Application.Avia.Airlines.Queries;

namespace TourCore.Application.Avia.Airlines.Handlers
{
    public class GetAirlineByIdHandler : IQueryHandler<GetAirlineByIdQuery, AirlineDto>
    {
        private readonly IAirlineRepository _airlineRepository;

        public GetAirlineByIdHandler(IAirlineRepository airlineRepository)
        {
            _airlineRepository = airlineRepository;
        }

        public async Task<AirlineDto> Handle(GetAirlineByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _airlineRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Airline was not found.");

            return entity.ToDto();
        }
    }
}