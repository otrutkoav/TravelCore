using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Cities.Commands;
using TourCore.Application.Cities.Mappings;
using TourCore.Application.Cities.Validators;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Geography.Cities;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Cities.Handlers
{
    public class CreateCityHandler : ICommandHandler<CreateCityCommand, CityDto>
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateCityCommandValidator _validator;

        public CreateCityHandler(
            ICityRepository cityRepository,
            ICountryRepository countryRepository,
            IRegionRepository regionRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateCityCommandValidator validator)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _regionRepository = regionRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<CityDto> Handle(CreateCityCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

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
                 cancellationToken))
            {
                throw new ConflictException(ErrorMessages.CityCodeExists, ErrorCode.CityCodeExists);
            }

            var entity = new City(
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

            await _cityRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}