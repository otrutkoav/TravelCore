using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Airports.Commands;
using TourCore.Contracts.Avia.Airports;
using TourCore.Application.Airports.Mappings;
using TourCore.Application.Airports.Validators;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Airports.Handlers
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
                throw new NotFoundException("Airport was not found.");

            var city = await _cityRepository.GetByIdAsync(command.CityId, cancellationToken);
            if (city == null)
                throw new NotFoundException("City was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _airportRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("Airport with same code already exists.");

            if (!string.IsNullOrWhiteSpace(command.IcaoCode))
            {
                var normalizedIcaoCode = command.IcaoCode.Trim().ToUpperInvariant();

                if (await _airportRepository.ExistsByIcaoCodeAsync(normalizedIcaoCode, command.Id, cancellationToken))
                    throw new ConflictException("Airport with same ICAO code already exists.");
            }

            entity.Update(
                command.CityId,
                normalizedCode,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.IcaoCode,
                command.LetterCode);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}