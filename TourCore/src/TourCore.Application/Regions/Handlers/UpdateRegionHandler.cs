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

namespace TourCore.Application.Regions.Handlers
{
    public class UpdateRegionHandler : ICommandHandler<UpdateRegionCommand, RegionDto>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateRegionCommandValidator _validator;

        public UpdateRegionHandler(
            IRegionRepository regionRepository,
            ICountryRepository countryRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateRegionCommandValidator validator)
        {
            _regionRepository = regionRepository;
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RegionDto> Handle(UpdateRegionCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _regionRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Region was not found.");

            if (!await _countryRepository.ExistsAsync(command.CountryId, cancellationToken))
                throw new NotFoundException("Country was not found.");

            var normalizedCode = command.Code.Trim().ToUpperInvariant();

            if (await _regionRepository.ExistsByCodeAsync(normalizedCode, command.Id, cancellationToken))
                throw new ConflictException("Region with the same code already exists.");

            entity.Update(
                command.CountryId,
                command.Name,
                _dateTimeProvider.UtcNow,
                command.NameEn,
                command.Code,
                command.SortOrder);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}