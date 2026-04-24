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

namespace TourCore.Application.VehiclePlans.Handlers
{
    public class UpdateVehiclePlanHandler : ICommandHandler<UpdateVehiclePlanCommand, VehiclePlanDto>
    {
        private readonly IVehiclePlanRepository _vehiclePlanRepository;
        private readonly ITransportRepository _transportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateVehiclePlanCommandValidator _validator;

        public UpdateVehiclePlanHandler(
            IVehiclePlanRepository vehiclePlanRepository,
            ITransportRepository transportRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateVehiclePlanCommandValidator validator)
        {
            _vehiclePlanRepository = vehiclePlanRepository;
            _transportRepository = transportRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<VehiclePlanDto> Handle(UpdateVehiclePlanCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _vehiclePlanRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Vehicle plan was not found.");

            var transport = await _transportRepository.GetByIdAsync(command.TransportId, cancellationToken);
            if (transport == null)
                throw new NotFoundException("Transport was not found.");

            entity.Update(
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

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}