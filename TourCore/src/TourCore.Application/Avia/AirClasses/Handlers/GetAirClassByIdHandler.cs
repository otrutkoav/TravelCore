using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.AirClasses.Mappings;
using TourCore.Application.Avia.AirClasses.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Avia.AirClasses;

namespace TourCore.Application.Avia.AirClasses.Handlers
{
    public class GetAirClassByIdHandler : IQueryHandler<GetAirClassByIdQuery, AirClassDto>
    {
        private readonly IAirClassRepository _repository;

        public GetAirClassByIdHandler(IAirClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<AirClassDto> Handle(GetAirClassByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(query.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException("AirClass not found");

            return entity.ToDto();
        }
    }
}