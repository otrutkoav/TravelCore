using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Regions.Commands;
using TourCore.Contracts.Geography.Regions;
using TourCore.Application.Regions.Mappings;
using TourCore.Application.Regions.Validators;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Regions.Handlers
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
                throw new NotFoundException("Country was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _regionRepository.ExistsByCodeAsync(normalizedCode, cancellationToken))
                throw new ConflictException("Region with the same code already exists.");

            var entity = new Region(
                command.CountryId,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.Code,
                command.SortOrder);

            await _regionRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}