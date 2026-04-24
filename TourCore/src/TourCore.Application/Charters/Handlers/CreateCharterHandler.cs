using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Charters.Commands;
using TourCore.Contracts.Avia.Charters;
using TourCore.Application.Charters.Mappings;
using TourCore.Application.Charters.Validators;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Charters.Handlers
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
                throw new NotFoundException("Departure city was not found.");

            var arrivalCity = await _cityRepository.GetByIdAsync(command.ArrivalCityId, cancellationToken);
            if (arrivalCity == null)
                throw new NotFoundException("Arrival city was not found.");

            if (!string.IsNullOrWhiteSpace(command.DepartureAirportCode))
            {
                var normalizedDepartureAirportCode = command.DepartureAirportCode.Trim().ToUpperInvariant();

                if (!await _airportRepository.ExistsByCodeValueAsync(normalizedDepartureAirportCode, cancellationToken))
                    throw new NotFoundException("Departure airport was not found.");
            }

            if (!string.IsNullOrWhiteSpace(command.ArrivalAirportCode))
            {
                var normalizedArrivalAirportCode = command.ArrivalAirportCode.Trim().ToUpperInvariant();

                if (!await _airportRepository.ExistsByCodeValueAsync(normalizedArrivalAirportCode, cancellationToken))
                    throw new NotFoundException("Arrival airport was not found.");
            }

            if (!string.IsNullOrWhiteSpace(command.AirlineCode))
            {
                var normalizedAirlineCode = command.AirlineCode.Trim().ToUpperInvariant();

                if (!await _airlineRepository.ExistsByCodeValueAsync(normalizedAirlineCode, cancellationToken))
                    throw new NotFoundException("Airline was not found.");
            }

            if (!string.IsNullOrWhiteSpace(command.AircraftCode))
            {
                var normalizedAircraftCode = command.AircraftCode.Trim().ToUpperInvariant();

                if (!await _aircraftRepository.ExistsByCodeValueAsync(normalizedAircraftCode, cancellationToken))
                    throw new NotFoundException("Aircraft was not found.");
            }

            if (!string.IsNullOrWhiteSpace(command.AirClassCode))
            {
                var normalizedAirClassCode = command.AirClassCode.Trim().ToUpperInvariant();

                if (!await _airClassRepository.ExistsByCodeValueAsync(normalizedAirClassCode, cancellationToken))
                    throw new NotFoundException("Air class was not found.");
            }

            var entity = new Charter(
                command.DepartureCityId,
                command.ArrivalCityId,
                command.FlightNumber,
                _dateTimeProvider.UtcNow,
                command.DepartureAirportCode,
                command.ArrivalAirportCode,
                command.AirlineCode,
                command.AircraftCode,
                command.AirClassCode,
                command.StopsCount,
                command.TimeChangesCode);

            await _charterRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}