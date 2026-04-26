using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Countries.Commands;
using TourCore.Application.Countries.Mappings;
using TourCore.Application.Countries.Validators;
using TourCore.Domain.Geography.Entities;
using TourCore.Contracts.Geography.Countries;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Countries.Handlers
{
    public class CreateCountryHandler : ICommandHandler<CreateCountryCommand, CountryDto>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateCountryCommandValidator _validator;

        public CreateCountryHandler(
            ICountryRepository countryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateCountryCommandValidator validator)
        {
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
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

            return entity.ToDto();
        }
    }
}