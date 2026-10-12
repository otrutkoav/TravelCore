using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Avia.Airports.Commands;
using TourCore.Application.Avia.Airports.Mappings;
using TourCore.Application.Avia.Airports.Validators;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Avia.Airports;

namespace TourCore.Application.Avia.Airports.Handlers
{
    public class UpdateAirportHandler : ICommandHandler<UpdateAirportCommand, AirportDto>
    {
        private readonly IAirportRepository _airportRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateAirportCommandValidator _validator;

        public UpdateAirportHandler(
            IAirportRepository airportRepository,
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateAirportCommandValidator validator)
        {
            _airportRepository = airportRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<AirportDto> Handle(UpdateAirportCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _airportRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.AirportNotFound, ErrorCode.AirportNotFound);

            var city = await _cityRepository.GetByIdAsync(command.CityId, cancellationToken);
            if (city == null)
                throw new NotFoundException(ErrorMessages.CityNotFound, ErrorCode.CityNotFound);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _airportRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException(ErrorMessages.AirportCodeExists, ErrorCode.AirportCodeExists);

            string normalizedIcaoCode = null;

            if (!string.IsNullOrWhiteSpace(command.IcaoCode))
            {
                normalizedIcaoCode = command.IcaoCode.Trim().ToUpperInvariant();

                if (await _airportRepository.ExistsByIcaoCodeAsync(normalizedIcaoCode, command.Id, cancellationToken))
                    throw new ConflictException(ErrorMessages.AirportIcaoCodeExists, ErrorCode.AirportIcaoCodeExists);
            }

            string normalizedLetterCode = null;

            if (!string.IsNullOrWhiteSpace(command.LetterCode))
                normalizedLetterCode = command.LetterCode.Trim().ToUpperInvariant();

            entity.Update(
                command.CityId,
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                normalizedIcaoCode,
                normalizedLetterCode);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}