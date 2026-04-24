using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Transfers.Commands;
using TourCore.Application.Transfers.DTOs;
using TourCore.Application.Transfers.Mappings;
using TourCore.Application.Transfers.Validators;

namespace TourCore.Application.Transfers.Handlers
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
                throw new NotFoundException("Transfer was not found.");

            if (command.CityId.HasValue)
            {
                var city = await _cityRepository.GetByIdAsync(command.CityId.Value, cancellationToken);
                if (city == null)
                    throw new NotFoundException("City was not found.");
            }

            if (command.DirectionId.HasValue)
            {
                var direction = await _transferDirectionRepository.GetByIdAsync(command.DirectionId.Value, cancellationToken);
                if (direction == null)
                    throw new NotFoundException("Transfer direction was not found.");
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