using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Cities.Commands;
using TourCore.Contracts.Geography.Cities;
using TourCore.Application.Cities.Mappings;
using TourCore.Application.Cities.Validators;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Cities.Handlers
{
    public class UpdateCityHandler : ICommandHandler<UpdateCityCommand, CityDto>
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateCityCommandValidator _validator;

        public UpdateCityHandler(
            ICityRepository cityRepository,
            ICountryRepository countryRepository,
            IRegionRepository regionRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateCityCommandValidator validator)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _regionRepository = regionRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<CityDto> Handle(UpdateCityCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _cityRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("City was not found.");

            if (!await _countryRepository.ExistsAsync(command.CountryId, cancellationToken))
                throw new NotFoundException("Country was not found.");

            if (command.RegionId.HasValue)
            {
                var region = await _regionRepository.GetByIdAsync(command.RegionId.Value, cancellationToken);

                if (region == null)
                    throw new NotFoundException("Region was not found.");

                if (region.CountryId != command.CountryId)
                    throw new ValidationException(new[] { "Region must belong to the selected country." });
            }

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _cityRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("City with the same code already exists.");

            entity.Update(
                command.CountryId,
                command.Name,
                command.NameEn,
                command.Code,
                command.SortOrder,
                command.IsDeparturePoint,
                _dateTimeProvider.UtcNow,
                command.TimeZone,
                command.IataCode,
                command.Coordinates,
                command.RegionId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}