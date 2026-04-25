using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.BusSchedules.Commands;
using TourCore.Contracts.Bus.BusSchedules;
using TourCore.Application.BusSchedules.Mappings;
using TourCore.Application.BusSchedules.Validators;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Bus.Entities;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Application.BusSchedules.Handlers
{
    public class CreateBusScheduleHandler : ICommandHandler<CreateBusScheduleCommand, BusScheduleDto>
    {
        private readonly IBusScheduleRepository _busScheduleRepository;
        private readonly IBusTransferRepository _busTransferRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateBusScheduleCommandValidator _validator;

        public CreateBusScheduleHandler(
            IBusScheduleRepository busScheduleRepository,
            IBusTransferRepository busTransferRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateBusScheduleCommandValidator validator)
        {
            _busScheduleRepository = busScheduleRepository;
            _busTransferRepository = busTransferRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<BusScheduleDto> Handle(CreateBusScheduleCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var busTransfer = await _busTransferRepository.GetByIdAsync(command.BusTransferId, cancellationToken);
            if (busTransfer == null)
                throw new NotFoundException("Bus transfer was not found.");

            var entity = new BusSchedule(
                command.BusTransferId,
                _dateTimeProvider.UtcNow,
                command.DateFrom,
                command.DateTo,
                command.TimeFrom,
                command.TimeTo,
                DaysOfWeek.FromLegacy(command.DaysOfWeek),
                command.DaysOnRoad);

            await _busScheduleRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}