using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Persistence.Transfers;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Transfers.Transfers.Commands;
using TourCore.Application.Transfers.Transfers.Mappings;
using TourCore.Application.Transfers.Transfers.Validators;
using TourCore.Contracts.Transfers.Transfers;

namespace TourCore.Application.Transfers.Transfers.Handlers
{
    public class UpdateTransferHandler : ICommandHandler<UpdateTransferCommand, TransferDto>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ITransferDirectionRepository _transferDirectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateTransferCommandValidator _validator;

        public UpdateTransferHandler(
            ITransferRepository transferRepository,
            ICityRepository cityRepository,
            ITransferDirectionRepository transferDirectionRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateTransferCommandValidator validator)
        {
            _transferRepository = transferRepository;
            _cityRepository = cityRepository;
            _transferDirectionRepository = transferDirectionRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<TransferDto> Handle(UpdateTransferCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _transferRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.TransferNotFound, ErrorCode.TransferNotFound);

            if (command.CityId.HasValue)
            {
                var city = await _cityRepository.GetByIdAsync(command.CityId.Value, cancellationToken);
                if (city == null)
                    throw new NotFoundException(ErrorMessages.CityNotFound, ErrorCode.CityNotFound);
            }

            if (command.DirectionId.HasValue)
            {
                var direction = await _transferDirectionRepository.GetByIdAsync(command.DirectionId.Value, cancellationToken);
                if (direction == null)
                    throw new NotFoundException(ErrorMessages.TransferDirectionNotFound, ErrorCode.TransferDirectionNotFound);
            }

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.TimeFrom,
                command.TimeTo,
                command.DurationText,
                command.PlaceFrom,
                command.PlaceTo,
                command.IsMain,
                command.CityId,
                command.DirectionId,
                command.Url,
                command.ShowOrder,
                command.AutoApplyFrom,
                command.AutoApplyTo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}