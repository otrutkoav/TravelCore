using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Countries.Commands;
using TourCore.Contracts.Geography.Countries;
using TourCore.Application.Countries.Mappings;
using TourCore.Application.Countries.Validators;

namespace TourCore.Application.Countries.Handlers
{
    public class UpdateCountryHandler : ICommandHandler<UpdateCountryCommand, CountryDto>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateCountryCommandValidator _validator;

        public UpdateCountryHandler(
            ICountryRepository countryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateCountryCommandValidator validator)
        {
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<CountryDto> Handle(UpdateCountryCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _countryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Country was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();
            var normalizedIsoCode2 = command.IsoCode2.Trim().ToUpperInvariant();
            var normalizedIsoCode3 = command.IsoCode3.Trim().ToUpperInvariant();

            if (await _countryRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("Country with the same code already exists.");

            if (await _countryRepository.ExistsByIsoCode2Async(normalizedIsoCode2, command.Id, cancellationToken))
                throw new ConflictException("Country with the same ISO2 code already exists.");

            if (await _countryRepository.ExistsByIsoCode3Async(normalizedIsoCode3, command.Id, cancellationToken))
                throw new ConflictException("Country with the same ISO3 code already exists.");

            entity.Update(
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

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}