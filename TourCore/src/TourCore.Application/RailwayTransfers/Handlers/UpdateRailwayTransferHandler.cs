using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RailwayTransfers.Commands;
using TourCore.Contracts.Railway.RailwayTransfers;
using TourCore.Application.RailwayTransfers.Mappings;
using TourCore.Application.RailwayTransfers.Validators;

namespace TourCore.Application.RailwayTransfers.Handlers
{
    public class UpdateRailwayTransferHandler : ICommandHandler<UpdateRailwayTransferCommand, RailwayTransferDto>
    {
        private readonly IRailwayTransferRepository _railwayTransferRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateRailwayTransferCommandValidator _validator;

        public UpdateRailwayTransferHandler(
            IRailwayTransferRepository railwayTransferRepository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateRailwayTransferCommandValidator validator)
        {
            _railwayTransferRepository = railwayTransferRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<RailwayTransferDto> Handle(UpdateRailwayTransferCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _railwayTransferRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Railway transfer was not found.");

            var countryFrom = await _countryRepository.GetByIdAsync(command.CountryFromId, cancellationToken);
            if (countryFrom == null)
                throw new NotFoundException("Departure country was not found.");

            var cityFrom = await _cityRepository.GetByIdAsync(command.CityFromId, cancellationToken);
            if (cityFrom == null)
                throw new NotFoundException("Departure city was not found.");

            var countryTo = await _countryRepository.GetByIdAsync(command.CountryToId, cancellationToken);
            if (countryTo == null)
                throw new NotFoundException("Arrival country was not found.");

            var cityTo = await _cityRepository.GetByIdAsync(command.CityToId, cancellationToken);
            if (cityTo == null)
                throw new NotFoundException("Arrival city was not found.");

            if (cityFrom.CountryId != command.CountryFromId)
                throw new ValidationException(new[] { "Departure city must belong to the selected departure country." });

            if (cityTo.CountryId != command.CountryToId)
                throw new ValidationException(new[] { "Arrival city must belong to the selected arrival country." });

            entity.Update(
                command.Name,
                command.CountryFromId,
                command.CityFromId,
                command.CountryToId,
                command.CityToId,
                _dateTimeProvider.UtcNow);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}