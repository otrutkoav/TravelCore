using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Seating;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Seating.VehiclePlans.Mappings;
using TourCore.Application.Seating.VehiclePlans.Queries;
using TourCore.Contracts.Seating.VehiclePlans;

namespace TourCore.Application.Seating.VehiclePlans.Handlers
{
    public class GetVehiclePlanByIdHandler : IQueryHandler<GetVehiclePlanByIdQuery, VehiclePlanDto>
    {
        private readonly IVehiclePlanRepository _vehiclePlanRepository;

        public GetVehiclePlanByIdHandler(IVehiclePlanRepository vehiclePlanRepository)
        {
            _vehiclePlanRepository = vehiclePlanRepository;
        }

        public async Task<VehiclePlanDto> Handle(GetVehiclePlanByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _vehiclePlanRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.VehiclePlanNotFound, ErrorCode.VehiclePlanNotFound);

            return entity.ToDto();
        }
    }
}