using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.VehiclePlans.Commands;
using TourCore.Application.VehiclePlans.DTOs;
using TourCore.Application.VehiclePlans.Mappings;
using TourCore.Application.VehiclePlans.Validators;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.VehiclePlans.Handlers
{
    public class CreateVehiclePlanHandler : ICommandHandler<CreateVehiclePlanCommand, VehiclePlanDto>
    {
        private readonly IVehiclePlanRepository _vehiclePlanRepository;
        private readonly ITransportRepository _transportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateVehiclePlanCommandValidator _validator;

        public CreateVehiclePlanHandler(
            IVehiclePlanRepository vehiclePlanRepository,
            ITransportRepository transportRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateVehiclePlanCommandValidator validator)
        {
            _vehiclePlanRepository = vehiclePlanRepository;
            _transportRepository = transportRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<VehiclePlanDto> Handle(CreateVehiclePlanCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var transport = await _transportRepository.GetByIdAsync(command.TransportId, cancellationToken);
            if (transport == null)
                throw new NotFoundException("Transport was not found.");

            var entity = new VehiclePlan(
                command.TransportId,
                command.RowsCount,
                command.ColumnsCount,
                command.AreaNumber,
                _dateTimeProvider.UtcNow,
                command.Name,
                command.PlanOrientation,
                command.IsAircraft,
                command.Dates,
                command.Comment);

            await _vehiclePlanRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}