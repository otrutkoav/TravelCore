using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.SeatingCells.Commands;
using TourCore.Application.SeatingCells.DTOs;
using TourCore.Application.SeatingCells.Mappings;
using TourCore.Application.SeatingCells.Validators;

namespace TourCore.Application.SeatingCells.Handlers
{
    public class UpdateSeatingCellHandler : ICommandHandler<UpdateSeatingCellCommand, SeatingCellDto>
    {
        private readonly ISeatingCellRepository _seatingCellRepository;
        private readonly IVehiclePlanRepository _vehiclePlanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateSeatingCellCommandValidator _validator;

        public UpdateSeatingCellHandler(
            ISeatingCellRepository seatingCellRepository,
            IVehiclePlanRepository vehiclePlanRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateSeatingCellCommandValidator validator)
        {
            _seatingCellRepository = seatingCellRepository;
            _vehiclePlanRepository = vehiclePlanRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<SeatingCellDto> Handle(UpdateSeatingCellCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _seatingCellRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Seating cell was not found.");

            var vehiclePlan = await _vehiclePlanRepository.GetByIdAsync(command.VehiclePlanId, cancellationToken);
            if (vehiclePlan == null)
                throw new NotFoundException("Vehicle plan was not found.");

            entity.Update(
                command.VehiclePlanId,
                command.Index,
                _dateTimeProvider.UtcNow,
                command.Number,
                command.Type,
                command.SeatsCount,
                command.Border);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}