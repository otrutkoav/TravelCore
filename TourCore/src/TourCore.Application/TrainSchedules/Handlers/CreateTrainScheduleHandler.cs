using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.TrainSchedules.Commands;
using TourCore.Contracts.Railway.TrainSchedules;
using TourCore.Application.TrainSchedules.Mappings;
using TourCore.Application.TrainSchedules.Validators;
using TourCore.Domain.Common.ValueObjects;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Application.TrainSchedules.Handlers
{
    public class CreateTrainScheduleHandler : ICommandHandler<CreateTrainScheduleCommand, TrainScheduleDto>
    {
        private readonly ITrainScheduleRepository _trainScheduleRepository;
        private readonly IRailwayTransferRepository _railwayTransferRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateTrainScheduleCommandValidator _validator;

        public CreateTrainScheduleHandler(
            ITrainScheduleRepository trainScheduleRepository,
            IRailwayTransferRepository railwayTransferRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateTrainScheduleCommandValidator validator)
        {
            _trainScheduleRepository = trainScheduleRepository;
            _railwayTransferRepository = railwayTransferRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<TrainScheduleDto> Handle(CreateTrainScheduleCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var railwayTransfer = await _railwayTransferRepository.GetByIdAsync(command.RailwayTransferId, cancellationToken);
            if (railwayTransfer == null)
                throw new NotFoundException("Railway transfer was not found.");

            var entity = new TrainSchedule(
                command.RailwayTransferId,
                _dateTimeProvider.UtcNow,
                command.DateFrom,
                command.DateTo,
                command.TimeFrom,
                command.TimeTo,
                DaysOfWeek.FromLegacy(command.DaysOfWeek),
                command.DaysOnRoad,
                command.Remark);

            await _trainScheduleRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}