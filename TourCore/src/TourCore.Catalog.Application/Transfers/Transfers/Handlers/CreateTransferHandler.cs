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
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Transfers.Transfers.Handlers
{
    public class CreateTransferHandler : ICommandHandler<CreateTransferCommand, TransferDto>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ITransferDirectionRepository _transferDirectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateTransferCommandValidator _validator;

        public CreateTransferHandler(
            ITransferRepository transferRepository,
            ICityRepository cityRepository,
            ITransferDirectionRepository transferDirectionRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateTransferCommandValidator validator)
        {
            _transferRepository = transferRepository;
            _cityRepository = cityRepository;
            _transferDirectionRepository = transferDirectionRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<TransferDto> Handle(CreateTransferCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

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

            var entity = new Transfer(
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

            await _transferRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}