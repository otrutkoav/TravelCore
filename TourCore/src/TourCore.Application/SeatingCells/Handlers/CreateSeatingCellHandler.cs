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
using TourCore.Domain.Seating.Entities;

namespace TourCore.Application.SeatingCells.Handlers
{
    public class CreateSeatingCellHandler : ICommandHandler<CreateSeatingCellCommand, SeatingCellDto>
    {
        private readonly ISeatingCellRepository _seatingCellRepository;
        private readonly IVehiclePlanRepository _vehiclePlanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateSeatingCellCommandValidator _validator;

        public CreateSeatingCellHandler(
            ISeatingCellRepository seatingCellRepository,
            IVehiclePlanRepository vehiclePlanRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateSeatingCellCommandValidator validator)
        {
            _seatingCellRepository = seatingCellRepository;
            _vehiclePlanRepository = vehiclePlanRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<SeatingCellDto> Handle(CreateSeatingCellCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var vehiclePlan = await _vehiclePlanRepository.GetByIdAsync(command.VehiclePlanId, cancellationToken);
            if (vehiclePlan == null)
                throw new NotFoundException("Vehicle plan was not found.");

            var entity = new SeatingCell(
                command.VehiclePlanId,
                command.Index,
                _dateTimeProvider.UtcNow,
                command.Number,
                command.Type,
                command.SeatsCount,
                command.Border);

            await _seatingCellRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}