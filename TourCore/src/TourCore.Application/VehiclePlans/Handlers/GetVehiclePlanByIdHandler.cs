using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.VehiclePlans.DTOs;
using TourCore.Application.VehiclePlans.Mappings;
using TourCore.Application.VehiclePlans.Queries;

namespace TourCore.Application.VehiclePlans.Handlers
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
                throw new NotFoundException("Vehicle plan was not found.");

            return entity.ToDto();
        }
    }
}