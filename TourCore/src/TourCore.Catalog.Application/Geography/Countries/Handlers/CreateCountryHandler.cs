using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Geography.Entities;
using TourCore.Contracts.Geography.Countries;
using TourCore.Application.Common.Errors;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Geography.Countries.Commands;
using TourCore.Application.Geography.Countries.Mappings;
using TourCore.Application.Geography.Countries.Validators;
using TourCore.Application.Abstractions.Caching;

namespace TourCore.Application.Geography.Countries.Handlers
{
    public class CreateCountryHandler : ICommandHandler<CreateCountryCommand, CountryDto>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateCountryCommandValidator _validator;
        private readonly ICatalogCacheUpdater _catalogCacheUpdater;

        public CreateCountryHandler(
            ICountryRepository countryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateCountryCommandValidator validator,
            ICatalogCacheUpdater catalogCacheUpdater)
        {
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
            _catalogCacheUpdater = catalogCacheUpdater;
        }

        public async Task<CountryDto> Handle(CreateCountryCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();
            var normalizedIsoCode2 = command.IsoCode2.Trim().ToUpperInvariant();
            var normalizedIsoCode3 = command.IsoCode3.Trim().ToUpperInvariant();

            if (await _countryRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException(ErrorMessages.CountryCodeExists, ErrorCode.CountryCodeExists);

            if (await _countryRepository.ExistsByIsoCode2Async(normalizedIsoCode2, cancellationToken))
                throw new ConflictException(ErrorMessages.CountryIsoCode2Exists, ErrorCode.CountryIsoCode2Exists);

            if (await _countryRepository.ExistsByIsoCode3Async(normalizedIsoCode3, cancellationToken))
                throw new ConflictException(ErrorMessages.CountryIsoCode3Exists, ErrorCode.CountryIsoCode3Exists);

            var entity = new Country(
                command.Name,
                command.NameEn,
                command.Code,
                command.IsoCode2,
                command.IsoCode3,
                command.SortOrder,
                _dateTimeProvider.UtcNow,
                command.DigitalCode,
                command.CitizenshipName,
                command.CitizenshipNameEn);

            await _countryRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _catalogCacheUpdater.CatalogChangedAsync(
                 CatalogCacheKeys.Countries,
                 cancellationToken);

            return entity.ToDto();
        }
    }
}