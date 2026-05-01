using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Aircrafts.Commands;
using TourCore.Application.Avia.Aircrafts.Mappings;
using TourCore.Application.Avia.Aircrafts.Validators;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Avia.Aircrafts.Handlers
{
    public class UpdateAircraftHandler : ICommandHandler<UpdateAircraftCommand, AircraftDto>
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateAircraftCommandValidator _validator;

        public UpdateAircraftHandler(
            IAircraftRepository aircraftRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateAircraftCommandValidator validator)
        {
            _aircraftRepository = aircraftRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AircraftDto> Handle(UpdateAircraftCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _aircraftRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(
                 ErrorMessages.AircraftNotFound,
                 ErrorCode.AircraftNotFound);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _aircraftRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException(
                    ErrorMessages.AircraftCodeExists,
                    ErrorCode.AircraftCodeExists);

            entity.Update(
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}