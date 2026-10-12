using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.AccommodationTypes.Commands;
using TourCore.Application.Hotels.AccommodationTypes.Mappings;
using TourCore.Application.Hotels.AccommodationTypes.Validators;
using TourCore.Contracts.Hotels.AccommodationTypes;

namespace TourCore.Application.Hotels.AccommodationTypes.Handlers
{
    public class UpdateAccommodationTypeHandler : ICommandHandler<UpdateAccommodationTypeCommand, AccommodationTypeDto>
    {
        private readonly IAccommodationTypeRepository _accommodationTypeRepository;
        private readonly IAccommodationPlacementRuleRepository _accommodationPlacementRuleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateAccommodationTypeCommandValidator _validator;

        public UpdateAccommodationTypeHandler(
            IAccommodationTypeRepository accommodationTypeRepository,
            IAccommodationPlacementRuleRepository accommodationPlacementRuleRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateAccommodationTypeCommandValidator validator)
        {
            _accommodationTypeRepository = accommodationTypeRepository;
            _accommodationPlacementRuleRepository = accommodationPlacementRuleRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AccommodationTypeDto> Handle(UpdateAccommodationTypeCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _accommodationTypeRepository.GetByIdAsync(command.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(ErrorMessages.AccommodationTypeNotFound, ErrorCode.AccommodationTypeNotFound);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _accommodationTypeRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException(ErrorMessages.AccommodationTypeCodeExists, ErrorCode.AccommodationTypeCodeExists);

            if (command.MainPlacementRuleId.HasValue)
            {
                var mainPlacementRule = await _accommodationPlacementRuleRepository.GetByIdAsync(
                    command.MainPlacementRuleId.Value,
                    cancellationToken);

                if (mainPlacementRule == null)
                    throw new NotFoundException(
                        ErrorMessages.AccommodationPlacementRuleNotFound,
                        ErrorCode.AccommodationPlacementRuleNotFound);
            }

            if (command.ExtraPlacementRuleId.HasValue)
            {
                var extraPlacementRule = await _accommodationPlacementRuleRepository.GetByIdAsync(
                    command.ExtraPlacementRuleId.Value,
                    cancellationToken);

                if (extraPlacementRule == null)
                    throw new NotFoundException(
                        ErrorMessages.AccommodationPlacementRuleNotFound,
                        ErrorCode.AccommodationPlacementRuleNotFound);
            }

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow,
                command.Code,
                command.NameEn,
                command.IsMain,
                command.AgeFrom,
                command.AgeTo,
                command.PerRoom,
                command.SortOrder,
                command.Description,
                command.MainPlacementRuleId,
                command.ExtraPlacementRuleId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}