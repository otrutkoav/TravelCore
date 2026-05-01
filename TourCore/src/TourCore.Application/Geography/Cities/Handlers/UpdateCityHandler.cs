using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Geography.Cities.Commands;
using TourCore.Application.Geography.Cities.Mappings;
using TourCore.Application.Geography.Cities.Validators;
using TourCore.Contracts.Geography.Cities;

namespace TourCore.Application.Geography.Cities.Handlers
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
                throw new NotFoundException(ErrorMessages.CityNotFound, ErrorCode.CityNotFound);

            if (!await _countryRepository.ExistsAsync(command.CountryId, cancellationToken))
                throw new NotFoundException(ErrorMessages.CountryNotFound, ErrorCode.CountryNotFound);

            if (command.RegionId.HasValue)
            {
                var region = await _regionRepository.GetByIdAsync(command.RegionId.Value, cancellationToken);

                if (region == null)
                    throw new NotFoundException(ErrorMessages.RegionNotFound, ErrorCode.RegionNotFound);

                if (region.CountryId != command.CountryId)
                    throw new ConflictException(ErrorMessages.RegionCountryMismatch, ErrorCode.RegionCountryMismatch);
            }

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _cityRepository.ExistsByCodeAsync(
                command.CountryId,
                normalizedCode,
                command.Id,
                cancellationToken))
            {
                throw new ConflictException(ErrorMessages.CityCodeExists, ErrorCode.CityCodeExists);
            }

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