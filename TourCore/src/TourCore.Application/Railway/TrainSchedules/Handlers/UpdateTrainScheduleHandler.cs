using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Railway.TrainSchedules;
using TourCore.Domain.Common.ValueObjects;
using TourCore.Application.Abstractions.Persistence.Railway;
using TourCore.Application.Railway.TrainSchedules.Commands;
using TourCore.Application.Railway.TrainSchedules.Mapping;
using TourCore.Application.Railway.TrainSchedules.Validators;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Railway.TrainSchedules.Handlers
{
    public class UpdateTrainScheduleHandler : ICommandHandler<UpdateTrainScheduleCommand, TrainScheduleDto>
    {
        private readonly ITrainScheduleRepository _trainScheduleRepository;
        private readonly IRailwayTransferRepository _railwayTransferRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateTrainScheduleCommandValidator _validator;

        public UpdateTrainScheduleHandler(
            ITrainScheduleRepository trainScheduleRepository,
            IRailwayTransferRepository railwayTransferRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateTrainScheduleCommandValidator validator)
        {
            _trainScheduleRepository = trainScheduleRepository;
            _railwayTransferRepository = railwayTransferRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<TrainScheduleDto> Handle(UpdateTrainScheduleCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _trainScheduleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.TrainScheduleNotFound, ErrorCode.TrainScheduleNotFound);

            var railwayTransfer = await _railwayTransferRepository.GetByIdAsync(command.RailwayTransferId, cancellationToken);
            if (railwayTransfer == null)
                throw new NotFoundException(ErrorMessages.RailwayTransferNotFound, ErrorCode.RailwayTransferNotFound);

            entity.Update(
                command.RailwayTransferId,
                _dateTimeProvider.UtcNow,
                command.DateFrom,
                command.DateTo,
                command.TimeFrom,
                command.TimeTo,
                DaysOfWeek.FromLegacy(command.DaysOfWeek),
                command.DaysOnRoad,
                command.Remark);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}