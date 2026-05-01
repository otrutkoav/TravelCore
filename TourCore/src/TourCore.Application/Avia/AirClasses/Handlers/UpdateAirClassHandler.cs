using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Avia.AirClasses.Commands;
using TourCore.Application.Avia.AirClasses.Mappings;
using TourCore.Application.Avia.AirClasses.Validators;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Avia.AirClasses;

namespace TourCore.Application.Avia.AirClasses.Handlers
{
    public class UpdateAirClassHandler : ICommandHandler<UpdateAirClassCommand, AirClassDto>
    {
        private readonly IAirClassRepository _airClassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateAirClassCommandValidator _validator;

        public UpdateAirClassHandler(
            IAirClassRepository airClassRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateAirClassCommandValidator validator)
        {
            _airClassRepository = airClassRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AirClassDto> Handle(UpdateAirClassCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _airClassRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.AirClassNotFound, ErrorCode.AirClassNotFound);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _airClassRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException(ErrorMessages.AirClassCodeExists, ErrorCode.AirClassCodeExists);

            entity.Update(
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.Group,
                command.SortOrder);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}