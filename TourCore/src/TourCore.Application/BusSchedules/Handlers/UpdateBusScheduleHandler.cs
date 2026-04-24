using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.BusSchedules.Commands;
using TourCore.Application.BusSchedules.DTOs;
using TourCore.Application.BusSchedules.Mappings;
using TourCore.Application.BusSchedules.Validators;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Application.BusSchedules.Handlers
{
    public class UpdateBusScheduleHandler : ICommandHandler<UpdateBusScheduleCommand, BusScheduleDto>
    {
        private readonly IBusScheduleRepository _busScheduleRepository;
        private readonly IBusTransferRepository _busTransferRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateBusScheduleCommandValidator _validator;

        public UpdateBusScheduleHandler(
            IBusScheduleRepository busScheduleRepository,
            IBusTransferRepository busTransferRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateBusScheduleCommandValidator validator)
        {
            _busScheduleRepository = busScheduleRepository;
            _busTransferRepository = busTransferRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<BusScheduleDto> Handle(UpdateBusScheduleCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _busScheduleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Bus schedule was not found.");

            var busTransfer = await _busTransferRepository.GetByIdAsync(command.BusTransferId, cancellationToken);
            if (busTransfer == null)
                throw new NotFoundException("Bus transfer was not found.");

            entity.Update(
                command.BusTransferId,
                _dateTimeProvider.UtcNow,
                command.DateFrom,
                command.DateTo,
                command.TimeFrom,
                command.TimeTo,
                DaysOfWeek.FromLegacy(command.DaysOfWeek),
                command.DaysOnRoad);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}