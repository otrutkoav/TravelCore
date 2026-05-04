using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Avia.Charters.Commands;
using TourCore.Application.Avia.Charters.Mappings;
using TourCore.Application.Avia.Charters.Validators;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Avia.Charters;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.Charters.Handlers
{
    public class CreateCharterHandler : ICommandHandler<CreateCharterCommand, CharterDto>
    {
        private readonly ICharterRepository _charterRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IAirlineRepository _airlineRepository;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAirClassRepository _airClassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateCharterCommandValidator _validator;

        public CreateCharterHandler(
            ICharterRepository charterRepository,
            ICityRepository cityRepository,
            IAirportRepository airportRepository,
            IAirlineRepository airlineRepository,
            IAircraftRepository aircraftRepository,
            IAirClassRepository airClassRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateCharterCommandValidator validator)
        {
            _charterRepository = charterRepository;
            _cityRepository = cityRepository;
            _airportRepository = airportRepository;
            _airlineRepository = airlineRepository;
            _aircraftRepository = aircraftRepository;
            _airClassRepository = airClassRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<CharterDto> Handle(CreateCharterCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var departureCity = await _cityRepository.GetByIdAsync(command.DepartureCityId, cancellationToken);
            if (departureCity == null)
                throw new NotFoundException(ErrorMessages.DepartureCityNotFound, ErrorCode.DepartureCityNotFound);

            var arrivalCity = await _cityRepository.GetByIdAsync(command.ArrivalCityId, cancellationToken);
            if (arrivalCity == null)
                throw new NotFoundException(ErrorMessages.ArrivalCityNotFound, ErrorCode.ArrivalCityNotFound);

            string normalizedDepartureAirportCode = null;

            if (!string.IsNullOrWhiteSpace(command.DepartureAirportCode))
            {
                normalizedDepartureAirportCode = command.DepartureAirportCode.Trim().ToUpperInvariant();

                if (!await _airportRepository.ExistsByCodeValueAsync(normalizedDepartureAirportCode, cancellationToken))
                    throw new NotFoundException(ErrorMessages.DepartureAirportNotFound, ErrorCode.DepartureAirportNotFound);
            }

            string normalizedArrivalAirportCode = null;

            if (!string.IsNullOrWhiteSpace(command.ArrivalAirportCode))
            {
                normalizedArrivalAirportCode = command.ArrivalAirportCode.Trim().ToUpperInvariant();

                if (!await _airportRepository.ExistsByCodeValueAsync(normalizedArrivalAirportCode, cancellationToken))
                    throw new NotFoundException(ErrorMessages.ArrivalAirportNotFound, ErrorCode.ArrivalAirportNotFound);
            }

            string normalizedAirlineCode = null;

            if (!string.IsNullOrWhiteSpace(command.AirlineCode))
            {
                normalizedAirlineCode = command.AirlineCode.Trim().ToUpperInvariant();

                if (!await _airlineRepository.ExistsByCodeValueAsync(normalizedAirlineCode, cancellationToken))
                    throw new NotFoundException(ErrorMessages.AirlineNotFound, ErrorCode.AirlineNotFound);
            }

            string normalizedAircraftCode = null;

            if (!string.IsNullOrWhiteSpace(command.AircraftCode))
            {
                normalizedAircraftCode = command.AircraftCode.Trim().ToUpperInvariant();

                if (!await _aircraftRepository.ExistsByCodeValueAsync(normalizedAircraftCode, cancellationToken))
                    throw new NotFoundException(ErrorMessages.AircraftNotFound, ErrorCode.AircraftNotFound);
            }

            string normalizedAirClassCode = null;

            if (!string.IsNullOrWhiteSpace(command.AirClassCode))
            {
                normalizedAirClassCode = command.AirClassCode.Trim().ToUpperInvariant();

                if (!await _airClassRepository.ExistsByCodeValueAsync(normalizedAirClassCode, cancellationToken))
                    throw new NotFoundException(ErrorMessages.AirClassNotFound, ErrorCode.AirClassNotFound);
            }

            string normalizedTimeChangesCode = null;

            if (!string.IsNullOrWhiteSpace(command.TimeChangesCode))
                normalizedTimeChangesCode = command.TimeChangesCode.Trim().ToUpperInvariant();

            var normalizedFlightNumber = command.FlightNumber.Trim().ToUpperInvariant();

            var entity = new Charter(
                command.DepartureCityId,
                command.ArrivalCityId,
                normalizedFlightNumber,
                _dateTimeProvider.UtcNow,
                normalizedDepartureAirportCode,
                normalizedArrivalAirportCode,
                normalizedAirlineCode,
                normalizedAircraftCode,
                normalizedAirClassCode,
                command.StopsCount,
                normalizedTimeChangesCode);

            await _charterRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}