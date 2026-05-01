using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Contracts.Hotels.AccommodationTypes;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Hotels.Entities;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Hotels.AccommodationTypes.Commands;
using TourCore.Application.Hotels.AccommodationTypes.Mappings;
using TourCore.Application.Hotels.AccommodationTypes.Validators;

namespace TourCore.Application.Hotels.AccommodationTypes.Handlers
{
    public class CreateAccommodationTypeHandler : ICommandHandler<CreateAccommodationTypeCommand, AccommodationTypeDto>
    {
        private readonly IAccommodationTypeRepository _accommodationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateAccommodationTypeCommandValidator _validator;

        public CreateAccommodationTypeHandler(
            IAccommodationTypeRepository accommodationTypeRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateAccommodationTypeCommandValidator validator)
        {
            _accommodationTypeRepository = accommodationTypeRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AccommodationTypeDto> Handle(CreateAccommodationTypeCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _accommodationTypeRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException("Accommodation type with same code already exists.");

            var entity = new AccommodationType(
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

            await _accommodationTypeRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}