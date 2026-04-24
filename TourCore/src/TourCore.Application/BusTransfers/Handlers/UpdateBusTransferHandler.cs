using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.BusTransfers.Commands;
using TourCore.Application.BusTransfers.DTOs;
using TourCore.Application.BusTransfers.Mappings;
using TourCore.Application.BusTransfers.Validators;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.BusTransfers.Handlers
{
    public class UpdateBusTransferHandler : ICommandHandler<UpdateBusTransferCommand, BusTransferDto>
    {
        private readonly IBusTransferRepository _busTransferRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateBusTransferCommandValidator _validator;

        public UpdateBusTransferHandler(
            IBusTransferRepository busTransferRepository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateBusTransferCommandValidator validator)
        {
            _busTransferRepository = busTransferRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<BusTransferDto> Handle(UpdateBusTransferCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _busTransferRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Bus transfer was not found.");

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