using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Aircrafts.Commands;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Application.Aircrafts.Mappings;
using TourCore.Application.Aircrafts.Validators;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Aircrafts.Handlers
{
    public class CreateAircraftHandler : ICommandHandler<CreateAircraftCommand, AircraftDto>
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateAircraftCommandValidator _validator;

        public CreateAircraftHandler(
            IAircraftRepository aircraftRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateAircraftCommandValidator validator)
        {
            _aircraftRepository = aircraftRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AircraftDto> Handle(CreateAircraftCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _aircraftRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException("Aircraft with same code already exists.");

            var entity = new Aircraft(
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn);

            await _aircraftRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}