using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.AccommodationTypes.Commands;
using TourCore.Contracts.Hotels.AccommodationTypes;
using TourCore.Application.AccommodationTypes.Mappings;
using TourCore.Application.AccommodationTypes.Validators;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.AccommodationTypes.Handlers
{
    public class UpdateAccommodationTypeHandler : ICommandHandler<UpdateAccommodationTypeCommand, AccommodationTypeDto>
    {
        private readonly IAccommodationTypeRepository _accommodationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateAccommodationTypeCommandValidator _validator;

        public UpdateAccommodationTypeHandler(
            IAccommodationTypeRepository accommodationTypeRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateAccommodationTypeCommandValidator validator)
        {
            _accommodationTypeRepository = accommodationTypeRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AccommodationTypeDto> Handle(UpdateAccommodationTypeCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _accommodationTypeRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Accommodation type was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _accommodationTypeRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("Accommodation type with same code already exists.");

            entity.Update(
                command.Name,
                _dateTimeProvider.UtcNow,
                normalizedCode,
                command.NameEn,
                command.IsMain,
                command.AgeFrom,
                command.AgeTo,
                command.PerRoom,
                command.SortOrder,
                command.Description,
                command.MainPlacementRule,
                command.ExtraPlacementRule);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}