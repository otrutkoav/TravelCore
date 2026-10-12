using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Bus.BusTransferPoints.Commands;
using TourCore.Application.Bus.BusTransferPoints.Mappings;
using TourCore.Application.Bus.BusTransferPoints.Validators;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Bus.BusTransferPoints;

namespace TourCore.Application.Bus.BusTransferPoints.Handlers
{
    public class UpdateBusTransferPointHandler : ICommandHandler<UpdateBusTransferPointCommand, BusTransferPointDto>
    {
        private readonly IBusTransferPointRepository _busTransferPointRepository;
        private readonly IBusTransferRepository _busTransferRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly UpdateBusTransferPointCommandValidator _validator;

        public UpdateBusTransferPointHandler(
            IBusTransferPointRepository busTransferPointRepository,
            IBusTransferRepository busTransferRepository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            UpdateBusTransferPointCommandValidator validator)
        {
            _busTransferPointRepository = busTransferPointRepository;
            _busTransferRepository = busTransferRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<BusTransferPointDto> Handle(UpdateBusTransferPointCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

            var entity = await _busTransferPointRepository.GetByIdAsync(command.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.BusTransferPointNotFound, ErrorCode.BusTransferPointNotFound);

            var busTransfer = await _busTransferRepository.GetByIdAsync(command.BusTransferId, cancellationToken);
            if (busTransfer == null)
                throw new NotFoundException(ErrorMessages.BusTransferNotFound, ErrorCode.BusTransferNotFound);

            var countryFrom = await _countryRepository.GetByIdAsync(command.CountryFromId, cancellationToken);
            if (countryFrom == null)
                throw new NotFoundException(ErrorMessages.DepartureCountryNotFound, ErrorCode.DepartureCountryNotFound);

            var cityFrom = await _cityRepository.GetByIdAsync(command.CityFromId, cancellationToken);
            if (cityFrom == null)
                throw new NotFoundException(ErrorMessages.DepartureCityNotFound, ErrorCode.DepartureCityNotFound);

            var countryTo = await _countryRepository.GetByIdAsync(command.CountryToId, cancellationToken);
            if (countryTo == null)
                throw new NotFoundException(ErrorMessages.ArrivalCountryNotFound, ErrorCode.ArrivalCountryNotFound);

            var cityTo = await _cityRepository.GetByIdAsync(command.CityToId, cancellationToken);
            if (cityTo == null)
                throw new NotFoundException(ErrorMessages.ArrivalCityNotFound, ErrorCode.ArrivalCityNotFound);

            if (cityFrom.CountryId != command.CountryFromId)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CityFromId", new[] { ErrorCode.DepartureCityCountryMismatch } }
                });
            }

            if (cityTo.CountryId != command.CountryToId)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CityToId", new[] { ErrorCode.ArrivalCityCountryMismatch } }
                });
            }

            entity.Update(
                command.BusTransferId,
                command.CountryFromId,
                command.CityFromId,
                command.CountryToId,
                command.CityToId,
                _dateTimeProvider.UtcNow,
                command.TimeFrom,
                command.TimeTo,
                command.DayFrom,
                command.DayTo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}