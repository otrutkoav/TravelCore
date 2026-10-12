using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Geography.Regions.Commands;
using TourCore.Application.Geography.Regions.Mappings;
using TourCore.Application.Geography.Regions.Validators;
using TourCore.Contracts.Geography.Regions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Regions.Handlers
{
    public class CreateRegionHandler : ICommandHandler<CreateRegionCommand, RegionDto>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateRegionCommandValidator _validator;

        public CreateRegionHandler(
            IRegionRepository regionRepository,
            ICountryRepository countryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateRegionCommandValidator validator)
        {
            _regionRepository = regionRepository;
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RegionDto> Handle(CreateRegionCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            if (!await _countryRepository.ExistsAsync(command.CountryId, cancellationToken))
                throw new NotFoundException(ErrorMessages.CountryNotFound, ErrorCode.CountryNotFound);

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _regionRepository.ExistsByCodeAsync(command.CountryId, normalizedCode, cancellationToken))
                throw new ConflictException(ErrorMessages.RegionCodeExists, ErrorCode.RegionCodeExists);

            var entity = new Region(
                command.CountryId,
                command.Name,
                command.Code,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.SortOrder);

            await _regionRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}