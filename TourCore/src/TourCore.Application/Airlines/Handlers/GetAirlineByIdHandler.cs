using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Avia.Airlines;
using TourCore.Application.Airlines.Mappings;
using TourCore.Application.Airlines.Queries;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Airlines.Handlers
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