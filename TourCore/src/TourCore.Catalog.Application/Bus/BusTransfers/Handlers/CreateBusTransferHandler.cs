using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Bus.BusTransfers.Commands;
using TourCore.Application.Bus.BusTransfers.Mappings;
using TourCore.Application.Bus.BusTransfers.Validators;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Bus.BusTransfers;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Application.Bus.BusTransfers.Handlers
{
    public class CreateBusTransferHandler : ICommandHandler<CreateBusTransferCommand, BusTransferDto>
    {
        private readonly IBusTransferRepository _busTransferRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CreateBusTransferCommandValidator _validator;

        public CreateBusTransferHandler(
            IBusTransferRepository busTransferRepository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            CreateBusTransferCommandValidator validator)
        {
            _busTransferRepository = busTransferRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _validator = validator;
        }

        public async Task<BusTransferDto> Handle(CreateBusTransferCommand command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);

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

            var entity = new BusTransfer(
                command.Name,
                command.CountryFromId,
                command.CityFromId,
                command.CountryToId,
                command.CityToId,
                _dateTimeProvider.UtcNow);

            await _busTransferRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.ToDto();
        }
    }
}